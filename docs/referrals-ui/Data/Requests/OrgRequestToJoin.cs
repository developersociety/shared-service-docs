using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OpenReferralPOV.Data.Requests
{
    public class OrgRequestToJoin
    {
        public Organization Org { get; set; }
        public bool IsAdmin { get; set; }
    }
}
