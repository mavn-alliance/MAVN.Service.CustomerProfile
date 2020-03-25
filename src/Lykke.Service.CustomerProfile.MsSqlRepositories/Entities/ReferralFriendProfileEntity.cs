using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Falcon.Common.Encryption;
using Lykke.Service.CustomerProfile.Domain.Models;

namespace Lykke.Service.CustomerProfile.MsSqlRepositories.Entities
{
    [Table("referral_friend_profiles")]
    public class ReferralFriendProfileEntity
    {
        public ReferralFriendProfileEntity()
        {
        }

        public ReferralFriendProfileEntity(ReferralFriendProfile referralFriendProfile)
        {
            Update(referralFriendProfile);
        }

        [Key]
        [Column("referral_friend_id")]
        public Guid ReferralFriendId { get; set; }

        [Column("referrer_id")]
        public Guid ReferrerId { get; set; }

        [Required]
        [Column("full_name")]
        [EncryptedProperty]
        public string FullName { get; set; }

        [Required]
        [Column("email")]
        [EncryptedProperty]
        public string Email { get; set; }

        internal void Update(ReferralFriendProfile referralFriendProfile)
        {
            ReferralFriendId = referralFriendProfile.ReferralFriendId;
            ReferrerId = referralFriendProfile.ReferrerId;
            FullName = referralFriendProfile.FullName;
            Email = referralFriendProfile.Email.ToLower();
        }
    }
}
