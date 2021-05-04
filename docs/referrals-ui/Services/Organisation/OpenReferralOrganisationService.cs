using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Identity.Web;
using Newtonsoft.Json;
using OpenReferralPOV.Data;
using OpenReferralPOV.Services.HttpClientAdapter;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace OpenReferralPOV.Services
{
    public class OpenReferralOrganisationService : IOpenReferralOrganisationService
    {
        private IHttpClientAdapter _httpClientAdapter;
        private readonly string _ApiBaseAddress = string.Empty;

        public OpenReferralOrganisationService(IHttpClientAdapter httpClientAdapter, IConfiguration configuration)
        {
            _httpClientAdapter = httpClientAdapter;
            _ApiBaseAddress = configuration["ORApi:BaseUrl"];
        }

        public async Task<IEnumerable<Organization>> GetAllOrganisations()
        {
            var responseString = await _httpClientAdapter.GetAsync(new Uri($"{ _ApiBaseAddress}/Organizations"));
            var organizations = JsonConvert.DeserializeObject<IEnumerable<Organization>>(responseString);

            return organizations;
        }

        public async Task<Organization> AddOrganization(Organization organization)
        {
            var responseString = await _httpClientAdapter.PostAsync(new Uri($"{ _ApiBaseAddress}/Organizations"), organization);
            var addedOrganization = JsonConvert.DeserializeObject<Organization>(responseString);

            //Confusing
            await _httpClientAdapter.GetAsync(new Uri($"{ _ApiBaseAddress}/KeyContact/{addedOrganization.Id}"));

            return addedOrganization;
        }

        public async Task RequestToJoinOrganisationAsMember(string orgId)
        {
            await _httpClientAdapter.GetAsync(new Uri($"{ _ApiBaseAddress}/OrganizationMember/create/{orgId}"));
        }

        public async Task RequestToJoinOrganisationAsAdmin(string orgId)
        {
            await _httpClientAdapter.GetAsync(new Uri($"{ _ApiBaseAddress}/KeyContact/admin/{orgId}"));
        }

        public async Task<Organization> GetOrganisation(string org)
        {
            var responseString = await _httpClientAdapter.GetAsync(new Uri($"{ _ApiBaseAddress}/Organizations/{org}"));
            var organization = JsonConvert.DeserializeObject<Organization>(responseString);
            return organization;
        }

        public async Task<Organization> UpdateOrganisation(Organization organization)
        {
            var responseString = await _httpClientAdapter.PutAsync(new Uri($"{ _ApiBaseAddress}/Organizations/{organization.Id}"), organization);
            return organization;
        }
    }
}
