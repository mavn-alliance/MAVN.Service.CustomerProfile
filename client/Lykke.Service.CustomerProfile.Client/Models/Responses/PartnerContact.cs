using JetBrains.Annotations;

namespace Lykke.Service.CustomerProfile.Client.Models.Responses
{
    /// <summary>
    /// Partner Contact.
    /// </summary>
    [PublicAPI]
    public class PartnerContact
    {
        /// <summary>
        /// Internal LocationId used to identify Contacts between different locations
        /// </summary>
        public string LocationId { get; set; }

        /// <summary>
        /// The Email of the Contact
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Contact First name
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Contact Last name
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// Contact phone number.
        /// </summary>
        public string PhoneNumber { get; set; }
    }
}
