using OpenReferralPOV.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OpenReferralPOV.Services
{
    public interface IOpenReferralMembershipRequestsService
    {
        Task<IEnumerable<MembershipRequests>> GetAllMemebershipRequestsThatCanBeActioned();

        Task<IEnumerable<MembershipRequests>> GetMembersOfEachOrg();
        Task<IEnumerable<MembershipRequests>> GetOrgJoinRequests();

        Task HandleDenyRequestForJoiningOrg(MembershipRequests requests);


        Task HandleGrantRequestForJoiningOrg(MembershipRequests requests);

        Task<IEnumerable<MembershipRequests>> GetAllMembersOfOrg(string orgId);
    }
}
