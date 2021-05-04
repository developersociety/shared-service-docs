using OpenReferralPOV.Data;
using OpenReferralPOV.Data.Enums;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OpenReferralPOV.Services
{
    public interface IOpenReferralService
    {
        Task<Location> AddLocation(Location location);        
        Task<Service> AddService(Service service);
        Task<IEnumerable<Service>> GetServicesAsync();
        Task<IEnumerable<Service>> GetServicesAsync(string postcode, double proximity);
        Task<Service> GetServiceById(string id);
        Task<Location> GetLocationById(string id);
        Task<Service> UpdateService(Service service);
    }
}