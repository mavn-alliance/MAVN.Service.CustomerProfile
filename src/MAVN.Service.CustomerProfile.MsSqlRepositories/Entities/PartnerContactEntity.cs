using System.ComponentModel.DataAnnotations.Schema;
using MAVN.Service.CustomerProfile.Domain.Models;

namespace MAVN.Service.CustomerProfile.MsSqlRepositories.Entities
{
    [Table("partner_contact")]
    public class PartnerContactEntity: PartnerContactBaseEntity
    {
        internal static PartnerContactEntity Create(IPartnerContact partnerContact)
        {
            return new PartnerContactEntity
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
