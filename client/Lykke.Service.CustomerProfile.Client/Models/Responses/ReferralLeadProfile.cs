using System;
using JetBrains.Annotations;

namespace Lykke.Service.CustomerProfile.Client.Models.Responses
{
    /// <summary>
    /// Represents referral lead profile.
    /// </summary>
    [PublicAPI]
    public class ReferralLeadProfile
    {
        /// <summary>
        /// The referral lead identifier.
        /// </summary>
        public Guid ReferralLeadId { get; set; }

        /// <summary>
        /// The referral lead first name.
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// The referral lead last name.
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// The referral lead phone number.
        /// </summary>
        public string PhoneNumber { get; set; }

        /// <summary>
        /// The referral lead email address.
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// The referral lead note.
        /// </summary>
        public string Note { get; set; }
    }
}
