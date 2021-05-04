using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OpenReferralPOV.Data
{
    public enum MembershipRequestsStatus
    {
        /// <summary>
        /// First status 
        ///     Users request to join 
        ///     Tthen if the org denies them it is set to Denied
        ///     Otherwise Member is allowed to join
        ///     At the end the user membership can be revoked
        /// </summary>
        REQUESTED,
        DENIED,
        JOINED,
        REVOKED
    }
}
