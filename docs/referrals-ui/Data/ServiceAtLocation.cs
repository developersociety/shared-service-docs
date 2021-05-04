using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OpenReferralPOV.Data
{
    public class ServiceAtLocation
    {
        public ServiceAtLocation()
        {
            Id = Guid.NewGuid().ToString();
        }
        public string Id { get; set; }
        public string Location_Id { get; set; }
        public string Service_Id { get; set; }
    }
}
