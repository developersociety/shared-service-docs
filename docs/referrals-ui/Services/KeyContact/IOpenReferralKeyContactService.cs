using OpenReferralPOV.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OpenReferralPOV.Services
{
    public interface IOpenReferralKeyContactService
    {
        Task RemoveKeyContact(KeyContact contact);

        Task<IEnumerable<KeyContact>> GetAllKeyContacts();

        Task<IEnumerable<KeyContact>> GetPendingAdminRequests();

        Task HandleAdminDenyRequest(string orgId, string userId);

        Task HandleAdminGrantRequest(string orgId, string userId);

        Task<IEnumerable<KeyContact>> GetOrgsICanManage();

        Task<IEnumerable<KeyContact>> GetOrgsIHaveRequestedToJoin();

        Task<IEnumerable<KeyContact>> GetKeyContactsForOrg(string orgId);
    }
}