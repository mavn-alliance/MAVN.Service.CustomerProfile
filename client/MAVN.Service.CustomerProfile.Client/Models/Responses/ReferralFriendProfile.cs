using System;
using JetBrains.Annotations;

namespace MAVN.Service.CustomerProfile.Client.Models.Responses
{
    /// <summary>
    /// Represents referral friend profile.
    /// </summary>
    [PublicAPI]
    public class ReferralFriendProfile
    {
        /// <summary>
        /// The referral friend identifier.
        /// </summary>
        public Guid ReferralFriendId { get; set; }

        /// <summary>
        /// The referrer identifier.
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
