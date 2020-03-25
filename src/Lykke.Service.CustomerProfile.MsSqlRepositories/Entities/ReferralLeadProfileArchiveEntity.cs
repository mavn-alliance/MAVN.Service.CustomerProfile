using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Falcon.Common.Encryption;

namespace Lykke.Service.CustomerProfile.MsSqlRepositories.Entities
{
    [Table("referral_lead_profiles_archive")]
    public class ReferralLeadProfileArchiveEntity
    {
        public ReferralLeadProfileArchiveEntity()
        {
        }

        public ReferralLeadProfileArchiveEntity(ReferralLeadProfileEntity entity)
        {
            ReferralLeadId = entity.ReferralLeadId;
            FirstName = entity.FirstName;
            LastName = entity.LastName;
            PhoneNumber = entity.PhoneNumber;
            Email = entity.Email;
            Note = entity.Note;
        }

        [Key]
        [Column("referral_lead_id")]
        public Guid ReferralLeadId { get; set; }

        [Required]
        [Column("first_name")]
        [EncryptedProperty]
        public string FirstName { get; set; }

        [Required]
        [Column("last_name")]
        [EncryptedProperty]
        public string LastName { get; set; }

        [Required]
        [Column("phone_number")]
        [EncryptedProperty]
        public string PhoneNumber { get; set; }

        [Required]
        [Column("email")]
        [EncryptedProperty]
        public string Email { get; set; }

        [Column("note")]
        [EncryptedProperty]
        public string Note { get; set; }
    }
}
