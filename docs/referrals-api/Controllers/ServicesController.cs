﻿using Microsoft.AspNetCore.Mvc;
using OpenReferrals.DataModels;
using System;
using System.Collections.Generic;
using OpenReferrals.Repositories;
using Microsoft.AspNetCore.Authorization;
using OpenReferrals.RegisterManagementConnector.ServiceClients;
using OpenReferrals.Repositories.OpenReferral;
using System.Threading.Tasks;
using OpenReferrals.Connectors.LocationSearchConnector.ServiceClients;
using System.Linq;
using Microsoft.Extensions.Options;
using OpenReferrals.Connectors.PostcodeConnector.ServiceClients;
using OpenReferrals.Sevices;
using OpenReferrals.Policies;

namespace OpenReferrals.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ServicesController : ControllerBase
    {
        private readonly IServiceRepository _serRepository;
        private readonly IPostcodeServiceClient _postcodeServiceClient;
        private readonly ILocationRepository _locationRepository;
        private readonly IRegisterManagmentServiceClient _registerManagmentServiceClient;
        private readonly ILocationSearchServiceClient _locationSearchServiceClient;
        private readonly IOrganisationMemberRepository _organisationMemberRepo;
        private readonly IKeyContactRepository _keyContactRepo;
        private readonly IAuthorizationService _authorizationService;

        public ServicesController(
            IServiceRepository serRepository,
            ILocationRepository locationRepository,
            IOrganisationMemberRepository organisationMemberRepository,
            IRegisterManagmentServiceClient registerManagmentServiceClient,
            ILocationSearchServiceClient locationSearchServiceClient,
            IPostcodeServiceClient postcodeServiceClient,
            IKeyContactRepository keyContactRepo,
            IAuthorizationService authorizationService
            )
        {
            _serRepository = serRepository;
            _locationRepository = locationRepository;
            _registerManagmentServiceClient = registerManagmentServiceClient;
            _locationSearchServiceClient = locationSearchServiceClient;
            _postcodeServiceClient = postcodeServiceClient;
            _organisationMemberRepo = organisationMemberRepository;
            _keyContactRepo = keyContactRepo;
            _authorizationService = authorizationService;
        }

        /// <summary>
        /// Get All Services
        /// </summary>
        /// <param name="text">Use text to perform a keyword search on services. This performs a full text search on the service title.</param>
        /// <param name="postcode">The postcode of the person who wishes to use the service. In order to find services that are within a reasonable distance.</param>
        /// <param name="proximity">The distance in metres that the person is willing to travel from the target postcode.</param>
        /// <returns>A <see cref="List{Service}"/>Returns all services based on input parameters</returns>
        [HttpGet]
        public async Task<IActionResult> Get([FromServices] IOptions<ApiBehaviorOptions> apiBehaviorOptions, string postcode = null, double? proximity = 5, string text = null)
        {
            var services = _serRepository.GetAll();

            if (postcode != null)
            {
                var isValid = await _postcodeServiceClient.ValidatePostcode(postcode);
                if (!isValid.Result)
                {
                    ModelState.AddModelError(nameof(Organisation.Id), "Postcode does not exist.");
                    return apiBehaviorOptions.Value.InvalidModelStateResponseFactory(ControllerContext);
                }
                var locationResults = await _locationSearchServiceClient.QueryLocations(postcode, proximity);
                var locationIds = locationResults.Select(res => res.id);
                //TODO: We only check for the first serviceAtLocation as we only save one, for now.
                services = services.ToList()
                    .FindAll(service => locationIds.Contains(service.Service_At_Locations.First().Location_Id));
            }

            if (text != null)
            {
                services = services.ToList().FindAll(service => service.Name.Contains(text));
            }

            return Ok(services);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Service service, [FromServices] IOptions<ApiBehaviorOptions> apiBehaviorOptions)
        {
            var userId = JWTAttributesService.GetSubject(Request);
            var authorizationResult = await _authorizationService.AuthorizeAsync(User, service.OrganizationId, AuthzPolicyNames.MustBeOrgAdmin);

            if (!authorizationResult.Succeeded)
            {
                return Forbid();
            }

            try
            {
                Guid.Parse(service.Id);
            }
            catch (Exception _)
            {
                ModelState.AddModelError(nameof(Service.Id), "Service Id is not a valid Guid");
                return apiBehaviorOptions.Value.InvalidModelStateResponseFactory(ControllerContext);
            }
            var publishedService = _registerManagmentServiceClient.CreateService(service);
            await _serRepository.InsertOne(publishedService);
            return Accepted(publishedService);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            var service = await _serRepository.FindById(id);
            return Ok(service);
        }

        [Authorize]
        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> Put([FromRoute] string id, [FromBody] Service service)
        {
            var authorizationResult = await _authorizationService.AuthorizeAsync(User, service.OrganizationId, AuthzPolicyNames.MustBeOrgAdmin);

            if (!authorizationResult.Succeeded)
            {
                return Forbid();
            }
            else
            {
                var updatedService = _registerManagmentServiceClient.UpdateService(service);

                await _serRepository.UpdateOne(updatedService);
                return Ok(updatedService);
            }
        }
    }
}