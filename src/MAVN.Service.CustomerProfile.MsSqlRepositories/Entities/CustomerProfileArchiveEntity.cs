using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Falcon.Common.Encryption;

namespace MAVN.Service.CustomerProfile.MsSqlRepositories.Entities
{
    [Table("customer_profile_archive")]
    public class CustomerProfileArchiveEntity
    {
        [Key]
        [Column("customer_id")]
        [Required]
        public string CustomerId { get; set; }

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

        [Column("short_phone_number")]
        [EncryptedProperty]
        public string ShortPhoneNumber { get; set; }

        [Column("country_phone_code_id")]
        [EncryptedProperty]
        public int? CountryPhoneCodeId { get; set; }

        [Column("tier_id")]
        public string TierId { get; set; }

        [Column("country_of_residence_id")]
        [EncryptedProperty]
        public int? CountryOfResidenceId { get; set; }

        [Column("country_of_nationality_id")]
        [EncryptedProperty]
        public int? CountryOfNationalityId { get; set; }

        [Column("email_verified")]
        public bool IsEmailVerified { get; set; }

        [Column("phone_verified")]
        public bool IsPhoneVerified { get; set; }

        [Column("was_phone_ever_verified")]
        public bool WasPhoneEverVerified { get; set; }

        [Column("registered_at")]
        [Required]
        public DateTime Registered { get; set; }

        internal static CustomerProfileArchiveEntity Create(CustomerProfileEntity customerProfile)
        {
            return new CustomerProfileArchiveEntity
            {
                CustomerId = customerProfile.CustomerId,
                Email = customerProfile.Email,
                FirstName = customerProfile.FirstName,
                LastName = customerProfile.LastName,
                PhoneNumber = customerProfile.PhoneNumber,
                ShortPhoneNumber = customerProfile.ShortPhoneNumber,
                CountryPhoneCodeId = customerProfile.CountryPhoneCodeId,
                CountryOfResidenceId = customerProfile.CountryOfResidenceId,
                CountryOfNationalityId = customerProfile.CountryOfNationalityId,
                TierId = customerProfile.TierId,
                Registered = customerProfile.Registered,
                IsEmailVerified = customerProfile.IsEmailVerified,
                IsPhoneVerified = customerProfile.IsPhoneVerified,
                WasPhoneEverVerified = customerProfile.WasPhoneEverVerified
            };
        }
    }
}
