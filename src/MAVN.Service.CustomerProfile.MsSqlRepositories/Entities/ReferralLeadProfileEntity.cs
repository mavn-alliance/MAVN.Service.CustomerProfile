using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MAVN.Common.Encryption;
using MAVN.Service.CustomerProfile.Domain.Models;

namespace MAVN.Service.CustomerProfile.MsSqlRepositories.Entities
{
    [Table("referral_lead_profiles")]
    public class ReferralLeadProfileEntity
    {
        public ReferralLeadProfileEntity()
        {
        }

        public ReferralLeadProfileEntity(ReferralLeadProfile referralLeadProfile)
        {
            Update(referralLeadProfile);
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

        internal void Update(ReferralLeadProfile referralLeadProfile)
        {
            ReferralLeadId = referralLeadProfile.ReferralLeadId;
            FirstName = referralLeadProfile.FirstName;
            LastName = referralLeadProfile.LastName;
            PhoneNumber = referralLeadProfile.PhoneNumber;
            Email = referralLeadProfile.Email;
            Note = referralLeadProfile.Note;
        }
    }
}
