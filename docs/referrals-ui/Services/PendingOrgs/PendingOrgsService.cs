using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using OpenReferralPOV.Data;
using OpenReferralPOV.Services.HttpClientAdapter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OpenReferralPOV.Services.PendingOrgs
{
    public class PendingOrgsService : IPendingOrgsService
    {
        private IHttpClientAdapter _httpClientAdapter;
        private readonly string _ApiBaseAddress = string.Empty;

        public PendingOrgsService(IHttpClientAdapter httpClientAdapter, IConfiguration configuration)
        {
            _httpClientAdapter = httpClientAdapter;
            _ApiBaseAddress = configuration["ORApi:BaseUrl"];
        }

        public async Task<IEnumerable<Organization>> GetPendingOrgs()
        {
            var responseString = await _httpClientAdapter.GetAsync(new Uri($"{ _ApiBaseAddress}/PendingOrganizations"));
            var organizations = JsonConvert.DeserializeObject<IEnumerable<Organization>>(responseString);

            return organizations;
        }

        public async Task<Organization> CreatePendingOrg(Organization organization)
        {
            var responseString = await _httpClientAdapter.PostAsync(new Uri($"{ _ApiBaseAddress}/PendingOrganizations"), organization);
            var addedOrganization = JsonConvert.DeserializeObject<Organization>(responseString);

            return addedOrganization;
        }

        public async Task<bool> UserHasPendingOrg()
        {
            var responseString = await _httpClientAdapter.GetAsync(new Uri($"{ _ApiBaseAddress}/PendingOrganizations/has-pending-org"));
            var hasPendingOrg = JsonConvert.DeserializeObject<bool>(responseString);

            return hasPendingOrg;
        }

        public async Task VerifyPendingOrganisation(string orgId)
        {
            await _httpClientAdapter.PostAsync(new Uri($"{ _ApiBaseAddress}/PendingOrganizations/{orgId}"), orgId);
        }

        public async Task DeletePendingOrganisation(string orgId)
        {
            await _httpClientAdapter.DeleteAsync(new Uri($"{ _ApiBaseAddress}/PendingOrganizations/{orgId}"), orgId);
        }
    }
}
