using OpenReferralPOV.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OpenReferralPOV.Services.PendingOrgs
{
    public interface IPendingOrgsService
    {
        Task<IEnumerable<Organization>> GetPendingOrgs();
        Task<Organization> CreatePendingOrg(Organization organization);
        Task VerifyPendingOrganisation(string orgId);
        Task<bool> UserHasPendingOrg();
        Task DeletePendingOrganisation(string orgId);
    }
}
