namespace MAVN.Service.CustomerProfile.Domain.Models
{
    public class PartnerContactModel : IPartnerContact
    {
        public string LocationId { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
    }
}
