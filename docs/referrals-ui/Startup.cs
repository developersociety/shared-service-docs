using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Identity.Web;
using Microsoft.Identity.Web.UI;
using OpenReferralPOV.Services;
using OpenReferralPOV.Services.HttpClientAdapter;
using OpenReferralPOV.Services.PendingOrgs;
using System;

namespace OpenReferralPOV
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMicrosoftIdentityWebAppAuthentication(Configuration, "AzureAdB2C")
                .EnableTokenAcquisitionToCallDownstreamApi(new string[] { Configuration["ORApi:Scope"] })
                .AddDistributedTokenCaches();
            //ToDo We should switch back to in memory token cache. The distributed cache is for debuggin only.
            //.AddInMemoryTokenCaches();

            services.AddDistributedMemoryCache();

            services.AddCors(options => options.AddDefaultPolicy(
                    builder => builder.AllowAnyHeader().AllowAnyOrigin().AllowAnyMethod()
                ));

            services.AddControllersWithViews()
                .AddMicrosoftIdentityUI();

            services.AddRazorPages();
            services.AddServerSideBlazor()
                .AddMicrosoftIdentityConsentHandler();
            
            services.Configure<OpenIdConnectOptions>(Configuration.GetSection("AzureAdB2C"));

            services.AddHttpClient<IOpenReferralOrganisationService, OpenReferralOrganisationService>(configureClient =>
            {
                configureClient.BaseAddress = new Uri(Configuration.GetSection("ORApi:BaseUrl").Value);
            });
            services.AddTransient<IOpenReferralOrganisationService, OpenReferralOrganisationService>();
            services.AddTransient<IPendingOrgsService, PendingOrgsService>();
            services.AddTransient<IOpenReferralKeyContactService, OpenReferralKeyContactService>();
            services.AddTransient<IOpenReferralMembershipRequestsService, OpenReferralMembershipRequestsService>();
            services.AddTransient<IOpenReferralPlaylistService, OpenReferralPlaylistService>();
            services.AddTransient<IOpenReferralServiceFilterService, OpenReferralMockServiceFilterService>();
            services.AddTransient<IOpenReferralService, OpenReferralService>();
            services.AddHttpContextAccessor();
            services.AddTransient<IHttpClientAdapter, HttpClientAdapter>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
                app.UseDeveloperExceptionPage();

            app.UseCors();
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapBlazorHub();
                endpoints.MapFallbackToPage("/_Host");
            });
        }
    }
}
