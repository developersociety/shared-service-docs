using OpenReferralPOV.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OpenReferralPOV.Services
{
    public static class ServiceHelperFunctions
    {
        public static Location BuildLocation(Service service, Location location)
        {
            location.Physical_Addresses.First().LocationId = location.Id;
            location.Physical_Addresses.First().Postal_Code = service._Postcode;
            location.Physical_Addresses.First().Address_1 = service.Address;
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
