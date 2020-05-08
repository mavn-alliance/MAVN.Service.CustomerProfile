using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MAVN.Common.Encryption;

namespace MAVN.Service.CustomerProfile.MsSqlRepositories.Entities
{
    [Table("referral_friend_profiles_archive")]
    public class ReferralFriendProfileArchiveEntity
    {
        public ReferralFriendProfileArchiveEntity()
        {
        }

        public ReferralFriendProfileArchiveEntity(ReferralFriendProfileEntity entity)
        {
            ReferralFriendId = entity.ReferralFriendId;
            ReferrerId = entity.ReferrerId;
            FullName = entity.FullName;
            Email = entity.Email.ToLower();
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
    }
}
