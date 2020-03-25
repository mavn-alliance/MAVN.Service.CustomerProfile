using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using Falcon.Common.Encryption;
using Lykke.Service.CustomerProfile.Domain.Enums;
using Lykke.Service.CustomerProfile.Domain.Models;

namespace Lykke.Service.CustomerProfile.MsSqlRepositories.Entities
{
    [Table("customer_profile")]
    public class CustomerProfileEntity
    {
        [Key]
        [Column("customer_id")]
        [Required]
        public string CustomerId { get; set; }

        [Column("email")]
        [Required]
        [EncryptedProperty]
        public string Email { get; set; }

        [Column("lower_cased_email")]
        [Required]
        [EncryptedProperty]
        public string LowerCasedEmail { get; set; }

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

        [Column("country_of_residence_id")]
        [EncryptedProperty]
        public int? CountryOfResidenceId { get; set; }

        [Column("country_of_nationality_id")]
        [EncryptedProperty]
        public int? CountryOfNationalityId { get; set; }

        [Column("tier_id")]
        public string TierId { get; set; }

        [Column("registered_at")]
        [Required]
        public DateTime Registered { get; set; }

        [Column("email_verified")]
        public bool IsEmailVerified { get; set; }

        [Column("was_email_ever_verified")]
        public bool WasEmailEverVerified { get; set; }

        [Column("phone_verified")]
        public bool IsPhoneVerified { get; set; }

        [Column("was_phone_ever_verified")]
        public bool WasPhoneEverVerified { get; set; }

        public virtual IList<LoginProviderEntity> LoginProviders { get; set; }

        public CustomerProfileStatus Status { get; set; }

        [NotMapped]
        public CustomerProfileErrorCodes ErrorCode { get; set; }

        internal static CustomerProfileEntity Create(ICustomerProfile customerProfile)
        {
            return new CustomerProfileEntity
            {
                CustomerId = customerProfile.CustomerId,
                Email = customerProfile.Email,
                LowerCasedEmail = customerProfile.Email.ToLower(),
                FirstName = customerProfile.FirstName,
                LastName = customerProfile.LastName,
                PhoneNumber = customerProfile.PhoneNumber,
                ShortPhoneNumber = customerProfile.ShortPhoneNumber,
                CountryPhoneCodeId = customerProfile.CountryPhoneCodeId,
                CountryOfNationalityId = customerProfile.CountryOfNationalityId,
                TierId = customerProfile.TierId,
                Registered = DateTime.UtcNow,
                IsEmailVerified = customerProfile.IsEmailVerified,
                IsPhoneVerified = customerProfile.IsPhoneVerified,
                LoginProviders = new List<LoginProviderEntity>(customerProfile.LoginProviders
                        .Select(x => new LoginProviderEntity
                        {
                            LoginProvider = x
                        })),
                Status = customerProfile.Status,
            };
        }
    }
}
