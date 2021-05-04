using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using OpenReferralPOV.Data;
using OpenReferralPOV.Services.HttpClientAdapter;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OpenReferralPOV.Services
{
    public class OpenReferralKeyContactService : IOpenReferralKeyContactService
    {
        private IHttpClientAdapter _httpClientAdapter;
        private readonly string _ApiBaseAddress = string.Empty;

        public OpenReferralKeyContactService(IHttpClientAdapter httpClientAdapter, IConfiguration configuration)
        {
            _httpClientAdapter = httpClientAdapter;
            _ApiBaseAddress = configuration["ORApi:BaseUrl"];
        }

        public async Task<IEnumerable<KeyContact>> GetAllKeyContacts()
        {
            var responseString = await _httpClientAdapter.GetAsync(new Uri($"{ _ApiBaseAddress}/KeyContact"));
            var keycontacts = JsonConvert.DeserializeObject<IEnumerable<KeyContact>>(responseString);

            return keycontacts;
        }

        public async Task<IEnumerable<KeyContact>> GetOrgsICanManage() {
            var responseString = await _httpClientAdapter.GetAsync(new Uri($"{ _ApiBaseAddress}/KeyContact/orgs-i-can-manage"));
            var keycontacts = JsonConvert.DeserializeObject<IEnumerable<KeyContact>>(responseString);
            return keycontacts;
        }
        public async Task<IEnumerable<KeyContact>> GetOrgsIHaveRequestedToJoin()
        {
            var responseString = await _httpClientAdapter.GetAsync(new Uri($"{ _ApiBaseAddress}/KeyContact/orgs-i-have-requested-to-join"));
            var keycontacts = JsonConvert.DeserializeObject<IEnumerable<KeyContact>>(responseString);
            return keycontacts;
        }

        public async Task RemoveKeyContact(KeyContact keycontact)
        {
            await _httpClientAdapter.DeleteAsync(new Uri($"{ _ApiBaseAddress}/KeyContact/delete"), keycontact);
        }

        public async Task<IEnumerable<KeyContact>> GetPendingAdminRequests()
        {
            var responseString = await _httpClientAdapter.GetAsync(new Uri($"{ _ApiBaseAddress}/KeyContact/admin/requests"));
            var keycontacts = JsonConvert.DeserializeObject<IEnumerable<KeyContact>>(responseString);

            return keycontacts;
        }

        public async Task HandleAdminGrantRequest(string orgId, string userId)
        {
            var responseString = await _httpClientAdapter.GetAsync(new Uri($"{ _ApiBaseAddress}/KeyContact/admin/confirm/{orgId}/{userId}"));
        }

        public async Task HandleAdminDenyRequest(string orgId, string userId)
        {
            var kc = new KeyContact() { OrgId = orgId, UserId = userId };
            var responseString = await _httpClientAdapter.DeleteAsync(new Uri($"{ _ApiBaseAddress}/KeyContact/delete"), kc);
        }

        


        public async Task<IEnumerable<KeyContact>> GetKeyContactsForOrg(string orgId)
        {
            var responseString = await _httpClientAdapter.GetAsync(new Uri($"{ _ApiBaseAddress}/KeyContact/orgs/{orgId}/contacts"));
            var keycontacts = JsonConvert.DeserializeObject<IEnumerable<KeyContact>>(responseString);
            return keycontacts;
        }
    }
}
