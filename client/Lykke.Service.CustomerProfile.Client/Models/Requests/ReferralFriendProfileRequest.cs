using System;
using JetBrains.Annotations;

namespace Lykke.Service.CustomerProfile.Client.Models.Requests
{
    /// <summary>
    /// Represents referral friend profile information.
    /// </summary>
    [PublicAPI]
    public class ReferralFriendProfileRequest
    {
        /// <summary>
        /// The referral friend identifier.
        /// </summary>
        public Guid ReferralFriendId { get; set; }

        /// <summary>
        /// The referral friend identifier.
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
