namespace Lykke.Service.CustomerProfile.Domain.Models
{
    public interface IPartnerContact
    {
        string LocationId { get; set; }
        string Email { get; set; }
        string FirstName { get; set; }
        string LastName { get; set; }
        string PhoneNumber { get; set; }
    }
}
