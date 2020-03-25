using System.ComponentModel.DataAnnotations.Schema;

namespace Lykke.Service.CustomerProfile.MsSqlRepositories.Entities
{
    [Table("partner_contact_archive")]
    public class PartnerContactArchiveEntity: PartnerContactBaseEntity
    {
        internal static PartnerContactArchiveEntity Create(PartnerContactEntity partnerContact)
        {
            return new PartnerContactArchiveEntity
            {
                LocationId = partnerContact.LocationId,
                Email = partnerContact.Email,
                FirstName = partnerContact.FirstName,
                LastName = partnerContact.LastName,
                PhoneNumber = partnerContact.PhoneNumber
            };
        }
    }
}
