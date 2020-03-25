using System;

namespace Lykke.Service.CustomerProfile.Domain.Models
{
    /// <summary>
    /// Represents referral friend profile.
    /// </summary>
    public class ReferralFriendProfile
    {
        /// <summary>
        /// The referral friend identifier.
        /// </summary>
        public Guid ReferralFriendId { get; set; }

        /// <summary>
        /// The referrer customer identifier.
        /// </summary>
        public Guid ReferrerId { get; set; }

        /// <summary>
        /// The referral friend full name.
        /// </summary>
        public string FullName { get; set; }

        /// <summary>
        /// The referral friend email address.
        /// </summary>
        public string Email { get; set; }
    }
}
