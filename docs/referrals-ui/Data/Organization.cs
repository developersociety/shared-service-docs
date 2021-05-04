using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OpenReferralPOV.Data
{
    public class Organization
    {
        public Organization()
        {
            Id = Guid.NewGuid().ToString();
            JoinAsMemberButtonDisabled = false;
            RequestToJoinStatusMessage = "Request to join";
            RequestToAdminister = "Request to Administer";
        }
        public string Id { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Description must be populated")]
        [StringLength(200, ErrorMessage = "Description can only be 200 chars long.")]
        public string Description { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Name must be populated")]
        [StringLength(100, ErrorMessage = "Name can only be 100 chars long.")]
        public string Name { get; set; }
        public int CharityNumber { get; set; }
        public IEnumerable<string> ServicesIds { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "URL must be populated")]
        [Url(ErrorMessage = "URL must be valid")]
        public string Url { get; set; }


        // Fields added to aid rendering
        public string RequestToJoinStatusMessage { get; set; }
        public string RequestToAdminister { get; set; }

        public bool JoinAsMemberButtonDisabled { get; set; }

        public bool JoinAsAdminButtonDisabled { get; set; }
    }
}
