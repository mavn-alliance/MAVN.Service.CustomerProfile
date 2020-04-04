using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Falcon.Common.Encryption;

namespace MAVN.Service.CustomerProfile.MsSqlRepositories.Entities
{
    public abstract class PartnerContactBaseEntity
    {
        [Key]
        [Column("location_id")]
        [Required]
        public string LocationId { get; set; }

        [Column("email")]
        [Required]
        [EncryptedProperty]
        public string Email { get; set; }

        [Column("first_name")]
        [EncryptedProperty]
        public string FirstName { get; set; }

        [Column("last_name")]
        [EncryptedProperty]
        public string LastName { get; set; }

        [Column("phone_number")]
        [EncryptedProperty]
        public string PhoneNumber { get; set; }
    }
}
