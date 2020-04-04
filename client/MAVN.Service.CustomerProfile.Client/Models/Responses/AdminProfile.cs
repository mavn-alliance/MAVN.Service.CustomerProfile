using System;
using JetBrains.Annotations;

namespace MAVN.Service.CustomerProfile.Client.Models.Responses
{
    /// <summary>
    /// Represents an admin profile.
    /// </summary>
    [PublicAPI]
    public class AdminProfile
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
