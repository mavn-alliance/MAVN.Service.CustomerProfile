using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Falcon.Common.Encryption;
using Lykke.Service.CustomerProfile.Domain.Models;

namespace Lykke.Service.CustomerProfile.MsSqlRepositories.Entities
{
    [Table("referral_hotel_profiles")]
    public class ReferralHotelProfileEntity
    {
        public ReferralHotelProfileEntity()
        {
        }

        public ReferralHotelProfileEntity(ReferralHotelProfile referralHotelProfile)
        {
            Update(referralHotelProfile);
        }
        
        [Key]
        [Column("referral_hotel_id")]
        public Guid ReferralHotelId { get; set; }

        [Required]
        [Column("email")]
        [EncryptedProperty]
        public string Email { get; set; }

        [Required]
        [Column("phone_number")]
        [EncryptedProperty]
        public string PhoneNumber { get; set; }

        [Required]
        [Column("name")]
        [EncryptedProperty]
        public string Name { get; set; }

        internal void Update(ReferralHotelProfile referralHotelProfile)
        {
            ReferralHotelId = referralHotelProfile.ReferralHotelId;
            Email = referralHotelProfile.Email;
            PhoneNumber = referralHotelProfile.PhoneNumber;
            Name = referralHotelProfile.Name;
        }
    }
}
