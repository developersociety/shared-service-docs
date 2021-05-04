using OpenReferralPOV.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OpenReferralPOV.Services
{
    public static class ServiceHelperFunctions
    {
        public static Location BuildLocation(Service service, PhysicalAddress address)
        {
            var location = new Location();
            address.LocationId = location.Id;
            address.Postal_Code = service._Postcode;
            address.Address_1 = service.Address;
            location.Physical_Addresses = new List<PhysicalAddress> { address };
            location.Name = service.Name;
            return location;
        }

        public static ServiceAtLocation BuildServiceAtLocation(Location location, Service service)
        {
            var serviceAtLocation = new ServiceAtLocation();
            serviceAtLocation.Location_Id = location.Id;
            serviceAtLocation.Service_Id = service.Id;

            return serviceAtLocation;
        }
    }
}
