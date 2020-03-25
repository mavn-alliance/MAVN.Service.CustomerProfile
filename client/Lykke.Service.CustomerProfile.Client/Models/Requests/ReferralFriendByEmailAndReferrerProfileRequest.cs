using System;
using JetBrains.Annotations;

namespace Lykke.Service.CustomerProfile.Client.Models.Requests
{
    /// <summary>
    /// Represents referral friend by email and referrer request information.
    /// </summary>
    [PublicAPI]
    public class ReferralFriendByEmailAndReferrerProfileRequest
    {
        /// <summary>
        /// The referral friend identifier.
        /// </summary>
        public Guid ReferrerId { get; set; }

        /// <summary>
        /// The referral friend email address.
        /// </summary>
        public string Email { get; set; }
    }
}
