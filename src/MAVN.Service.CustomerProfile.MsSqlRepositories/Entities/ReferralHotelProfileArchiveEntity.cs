using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MAVN.Common.Encryption;

namespace MAVN.Service.CustomerProfile.MsSqlRepositories.Entities
{
    [Table("referral_hotel_profiles_archive")]
    public class ReferralHotelProfileArchiveEntity
    {
        public ReferralHotelProfileArchiveEntity()
        {
        }

        public ReferralHotelProfileArchiveEntity(ReferralHotelProfileEntity entity)
        {
            ReferralHotelId = entity.ReferralHotelId;
            Email = entity.Email;
        }

        [Key]
        [Column("referral_hotel_id")]
        public Guid ReferralHotelId { get; set; }

        [Required]
        [Column("email")]
        [EncryptedProperty]
        public string Email { get; set; }
    }
}
