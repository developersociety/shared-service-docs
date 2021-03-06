using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Microsoft.Identity.Web;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.IO;
using OpenReferrals.RegisterManagementConnector.Extensions;
using OpenReferrals.RegisterManagementConnector.Configuration;
using System.Text.Json.Serialization;
using OpenReferrals.Repositories.Configuration;
using OpenReferrals.Repositories.Common;
using OpenReferrals.Repositories.OpenReferral;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using OpenReferrals.Sendgrid;
using OpenReferrals.Connectors.Common;
using OpenReferrals.Connectors.PostcodeConnector.ServiceClients;
using Microsoft.Azure.Search;
using OpenReferrals.Connectors.LocationSearchConnector.ServiceClients;
using OpenReferrals.Policies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using OpenReferrals.Repositories.OpenReferral.PendingOrgs;

namespace OpenReferrals
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            InjectAuth(services);


            var mongoOptions = new MongoDbSettings();
            Configuration.Bind("MongoDbSettings", mongoOptions);
            services.AddSingleton(mongoOptions);


            var sendgridsettings = new SendGridSettings();
            Configuration.Bind("Sendgrid", sendgridsettings);
            services.AddSingleton(sendgridsettings);

            services.AddScoped(typeof(IMongoRepository<>), typeof(MongoRepository<>));
            services.AddTransient<IOrganisationRepository, OrganisationRepository>();
            services.AddTransient<IPendingOrganisationRepository, PendingOrganisationRepository>();
            services.AddTransient<IKeyContactRepository, KeyContactRepository>();
            services.AddTransient<IOrganisationMemberRepository, OrganisationMemberRepository>();
            services.AddTransient<IPlaylistRepository, PlaylistRepository>();
            services.AddTransient<IServiceRepository, ServiceRepository>();
            services.AddTransient<ILocationRepository, LocationRepository>();
            services.AddTransient<IUnAuthenticatedHttpAdapter, UnAuthenticatedHttpAdapter>();
            services.AddTransient<IPostcodeServiceClient, PostcodeServiceClient>();
            services.AddTransient<ILocationSearchServiceClient, LocationSearchServiceClient>();

            services.AddTransient<ISendGridSender, SendGridSender>();

            AddSearchIndexClient(services);

            var registerOptions = new RegisterManagmentOptions();
            Configuration.Bind("RegisterManagementAPI", registerOptions);
            services.InjectRegisterManagementServiceClient(registerOptions);


            ApplySwaggerGen(services);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
            }
            else
            {
                app.UseExceptionHandler("/error");
            }

            app.UseExceptionHandler("/error-local-development");
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "OpenReferrals v1");
            }
            );

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        private void InjectAuth(IServiceCollection services)
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddMicrosoftIdentityWebApi(Configuration, "AzureAdB2C")
                .EnableTokenAcquisitionToCallDownstreamApi()
                .AddInMemoryTokenCaches();

            services.AddAuthorization(options =>
            {
                options.AddPolicy("MustBeOrgAdmin", policy =>
                    policy.Requirements.Add(new OrganisationAdminRequirement()));
            });

            services.AddTransient<IAuthorizationHandler, OrganisationAdminHandler>();
            services.AddTransient<IActionContextAccessor, ActionContextAccessor>();
        }

        private void ApplySwaggerGen(IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Open Referrals API(Cast Project)",
                    Description = "Allows users to query Orgs, Services and Locations. Data can be added easily using the related OpenReferralUI project to sign up and add data.",
                    Contact = new OpenApiContact
                    {
                        Name = "Catalyst",
                        Email = "support@wearecast.org.uk",
                        Url = new Uri("https://www.thecatalyst.org.uk/"),
                    },
                });
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = @"Note that a token can only be created via the correct AzureB2C tenant. JWT Authorization header using the Bearer scheme. Enter 'Bearer' [space] and then your token in the text input below. Example: 'Bearer 12345abcdef'",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement()
                  {
                    {
                      new OpenApiSecurityScheme
                      {
                        Reference = new OpenApiReference
                          {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                          },
                          Scheme = "oauth2",
                          Name = "Bearer",
                          In = ParameterLocation.Header,

                        },
                        new List<string>()
                      }
                    });

                // Set the comments path for the Swagger JSON and UI.
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });
        }

        private void AddSearchIndexClient(IServiceCollection services)
        {
            string serviceName = Configuration["AzureSearch:ServiceName"];
            string queryKey = Configuration["AzureSearch:PrimaryKey"];
            string indexName = Configuration["AzureSearch:IndexName"];
            SearchIndexClient searchIndexClient = new SearchIndexClient(serviceName, indexName, new SearchCredentials(queryKey));
            services.AddSingleton<ISearchIndexClient>(searchIndexClient);
        }
    }
}