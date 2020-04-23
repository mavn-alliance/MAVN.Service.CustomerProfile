using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Falcon.Common.Encryption;

namespace MAVN.Service.CustomerProfile.MsSqlRepositories.Entities
{
    [Table("admin_profiles_archive")]
    public class AdminProfileArchiveEntity
    {
        public AdminProfileArchiveEntity()
        {
        }

        internal AdminProfileArchiveEntity(AdminProfileEntity entity)
        {
            AdminId = entity.AdminId;
            FirstName = entity.FirstName;
            LastName = entity.LastName;
            Email = entity.Email;
            IsEmailVerified = entity.IsEmailVerified;
            WasEmailEverVerified = entity.WasEmailEverVerified;
            PhoneNumber = entity.PhoneNumber;
            Company = entity.Company;
            Department = entity.Department;
            JobTitle = entity.JobTitle;
        }

        [Key]
        [Column("admin_id")]
        public Guid AdminId { get; set; }

        [Required]
        [Column("first_name")]
        [EncryptedProperty]
        public string FirstName { get; set; }

        [Required]
        [Column("last_name")]
        [EncryptedProperty]
        public string LastName { get; set; }

        [Required]
        [Column("email")]
        [EncryptedProperty]
        public string Email { get; set; }

        [Column("email_verified")]
        public bool IsEmailVerified { get; set; }

        [Column("was_email_ever_verified")]
        public bool WasEmailEverVerified { get; set; }

        [Required]
        [Column("phone_number")]
        [EncryptedProperty]
        public string PhoneNumber { get; set; }

        [Required]
        [Column("company")]
        [EncryptedProperty]
        public string Company { get; set; }

        [Required]
        [Column("department")]
        [EncryptedProperty]
        public string Department { get; set; }

        [Required]
        [Column("job_title")]
        [EncryptedProperty]
        public string JobTitle { get; set; }
    }
}
