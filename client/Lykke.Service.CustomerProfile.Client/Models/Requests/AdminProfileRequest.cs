using System;
using JetBrains.Annotations;

namespace Lykke.Service.CustomerProfile.Client.Models.Requests
{
    /// <summary>
    /// Represents admin profile information.
    /// </summary>
    [PublicAPI]
    public class AdminProfileRequest
    {
        /// <summary>
        /// The unique identifier.
        /// </summary>
        public Guid AdminId { get; set; }

        /// <summary>
        /// The first name.
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// The last name.
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// The email address.
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Phone number.
        /// </summary>
        public string PhoneNumber { get; set; }

        /// <summary>
        /// Company.
        /// </summary>
        public string Company { get; set; }

        /// <summary>
        /// Department.
        /// </summary>
        public string Department { get; set; }

        /// <summary>
        /// Job title.
        /// </summary>
        public string JobTitle { get; set; }
    }
}
