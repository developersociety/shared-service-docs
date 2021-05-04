using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace OpenReferralPOV.Data
{
    public class Service
    {
        public Service()
        {
            Id = Guid.NewGuid().ToString();
            Regular_Schedules = new List<RegularSchedule>();
            Service_At_Locations = new List<ServiceAtLocation>();
            Contacts = new List<string>();

            Tags = new List<int>();
        }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Description must be populated")]
        [StringLength(200, ErrorMessage = "Description can only be 200 chars long.")]
        public string Description { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Email must be populated")]
        [EmailAddress(ErrorMessage = "Email must be valid")]
        public string Email { get; set; }
        public string Id { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Name must be populated")]
        [StringLength(100, ErrorMessage = "Name can only be 100 chars long.")]
        public string Name { get; set; }
        public string OrganizationId { get; set; }
        public IEnumerable<RegularSchedule> Regular_Schedules { get; set; }
        public IEnumerable<ServiceAtLocation> Service_At_Locations { get; set; }
        public IEnumerable<string> Contacts { get; set; }
        public string Status { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "URL must be populated")]
        [Url(ErrorMessage = "URL must be valid")]
        public string Url { get; set; }
        //Postcode is only here because the front end validation is not picking it up, as a sub attribute of service
        [JsonIgnore]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Postcode must be populated")]
        [RegularExpression(@"(GIR 0AA)|((([A-Z-[QVX]][0-9][0-9]?)|(([A-Z-[QVX]][A-Z-[IJZ]][0-9][0-9]?)|(([A-Z-[QVX]][0-9][A-HJKSTUW])|([A-Z-[QVX]][A-Z-[IJZ]][0-9][ABEHMNPRVWXY])))) [0-9][A-Z-[CIKMOV]]{2})",
         ErrorMessage = "The Postcode must be a valid UK Postcode.")]
        public string _Postcode { get; set; }

        //address is only here because the front end validation is not picking it up, as a sub attribute of service
        [Required(AllowEmptyStrings = false, ErrorMessage = "Address must be populated")]
        [StringLength(20, ErrorMessage = "Address can only be 20 chars long.")]
        public string Address { get; set; }
        public IEnumerable<int> Tags { get; set; }
        public int? Category { get; set; }
    }
}
