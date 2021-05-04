namespace OpenReferralPOV.Data
{
    public class KeyContact
    {
        public KeyContact()
        {
            DisabledButton = false;
        }
        public string Id { get; set; }
        public string UserId { get; set; }
        public string OrgId { get; set; }
        public string OrgName { get; set; }
        public string UserEmail { get; set; }

        public bool IsAdmin { get; set; }
        public bool IsPending { get; set; }

        public bool DisabledButton { get; set; }
    }
}
