using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OpenReferralPOV.Data
{
    public class MembershipRequests
    {
        public MembershipRequests()
        {
            DisableButton = false;
        }
        public string Id { get; set; }
        public string UserId { get; set; }
        public string OrgId { get; set; }
        public string Email { get; set; }

        public MembershipRequestsStatus Status { get; set; }

        // View 
        public bool DisableButton { get; set; }
    }
}
