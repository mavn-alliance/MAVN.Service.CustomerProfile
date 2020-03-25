using System;
using JetBrains.Annotations;

namespace Lykke.Service.CustomerProfile.Client.Models.Responses
{
    /// <summary>
    /// Represents referral hotel profile.
    /// </summary>
    [PublicAPI]
    public class ReferralHotelProfile
    {
        /// <summary>
        /// The referral hotel identifier.
        /// </summary>
        public Guid ReferralHotelId { get; set; }

        /// <summary>
        /// The referral hotel email address.
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// The referral hotel phone number.
        /// </summary>
        public string PhoneNumber { get; set; }

        /// <summary>
        /// The referral hotel name.
        /// </summary>
        public string Name { get; set; }
    }
}
