using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using OpenReferralPOV.Data;
using OpenReferralPOV.Services.HttpClientAdapter;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OpenReferralPOV.Services
{
    public class OpenReferralMembershipRequestsService : IOpenReferralMembershipRequestsService
    {
        private IHttpClientAdapter _httpClientAdapter;
        private readonly string _ApiBaseAddress = string.Empty;

        public OpenReferralMembershipRequestsService(IHttpClientAdapter httpClientAdapter, IConfiguration configuration)
        {
            _httpClientAdapter = httpClientAdapter;
            _ApiBaseAddress = configuration["ORApi:BaseUrl"];
        }

        public async Task<IEnumerable<MembershipRequests>> GetAllMemebershipRequestsThatCanBeActioned()
        {
            var responseString = await _httpClientAdapter.GetAsync(new Uri($"{ _ApiBaseAddress}/OrganizationMember/my/requests"));
            var requests = JsonConvert.DeserializeObject<IEnumerable<MembershipRequests>>(responseString);
            return requests;
        }

        public async Task<IEnumerable<MembershipRequests>> GetMembersOfEachOrg()
        {
            var responseString = await _httpClientAdapter.GetAsync(new Uri($"{ _ApiBaseAddress}/OrganizationMember/all/joined"));
            var requests = JsonConvert.DeserializeObject<IEnumerable<MembershipRequests>>(responseString);
            return requests;
        }

        public async Task<IEnumerable<MembershipRequests>> GetOrgJoinRequests()
        {
            var responseString = await _httpClientAdapter.GetAsync(new Uri($"{ _ApiBaseAddress}/OrganizationMember/admin/requests"));
            var requests = JsonConvert.DeserializeObject<IEnumerable<MembershipRequests>>(responseString);
            return requests;
        }


        public async Task<IEnumerable<MembershipRequests>> GetAllMembersOfOrg(string orgId)
        {
            var responseString = await _httpClientAdapter.GetAsync(new Uri($"{ _ApiBaseAddress}/OrganizationMember/all/{orgId}"));
            var requests = JsonConvert.DeserializeObject<IEnumerable<MembershipRequests>>(responseString);
            return requests;
        }

        public async Task HandleGrantRequestForJoiningOrg(MembershipRequests requests)
        {
            await _httpClientAdapter.GetAsync(new Uri($"{ _ApiBaseAddress}/OrganizationMember/grant/{requests.OrgId}/{requests.UserId}"));
        }

        public async Task HandleDenyRequestForJoiningOrg(MembershipRequests requests)
        {
            await _httpClientAdapter.GetAsync(new Uri($"{ _ApiBaseAddress}/OrganizationMember/deny/{requests.OrgId}/{requests.UserId}"));
        }
    }
}
