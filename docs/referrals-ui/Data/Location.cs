using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OpenReferralPOV.Data
{
    public class Location
    {
        public Location()
        {
            Id = Guid.NewGuid().ToString();
            Physical_Addresses = new List<PhysicalAddress>();
            IsVulnerable = false;
        }
        public string Id { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Name must be populated")]
        [StringLength(200, ErrorMessage = "Description can only be 200 chars long.")]
        public string Description { get; set; }
        public bool IsVulnerable { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Name must be populated")]
        [StringLength(50, ErrorMessage = "Name can only be 50 chars long.")]
        public string Name { get; set; }
        public IEnumerable<PhysicalAddress> Physical_Addresses { get; set; }
    }
}
