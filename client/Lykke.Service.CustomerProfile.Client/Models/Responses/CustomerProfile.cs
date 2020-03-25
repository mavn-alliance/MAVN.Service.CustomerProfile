using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using Lykke.Service.CustomerProfile.Client.Models.Enums;

namespace Lykke.Service.CustomerProfile.Client.Models.Responses
{
    /// <summary>
    /// Customer profile.
    /// </summary>
    [PublicAPI]
    public class CustomerProfile
    {
        /// <summary>
        /// Internal CustomerId used to identify Customers between different components/microservices
        /// </summary>
        public string CustomerId { get; set; }

        /// <summary>
        /// The Email of the Customer
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Customer First name
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Customer Last name
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// The phone number with country dialing code.
        /// </summary>
        public string PhoneNumber { get; set; }

        /// <summary>
        /// The phone number with out country dialing code. 
        /// </summary>
        public string ShortPhoneNumber { get; set; }

        /// <summary>
        /// Date of registration/entity creation
        /// </summary>
        public DateTime Registered { get; set; }

        /// <summary>
        /// Email verification flag
        /// </summary>
        public bool IsEmailVerified { get; set; }

        /// <summary>
        /// Phone verification flag
        /// </summary>
        public bool IsPhoneVerified { get; set; }

        /// <summary>
        /// The phone country dialing code identifier.
        /// </summary>
        public int? CountryPhoneCodeId { get; set; }

        /// <summary>
        /// The country of residence identifier.
        /// </summary>
        public int CountryOfResidenceId { get; set; }

        /// <summary>
        /// The country of nationality identifier.
        /// </summary>
        /// Intentionally not nullable to avoid breaking changes. It will return 0 if it is null in the DB
        public int CountryOfNationalityId { get; set; }

        /// <summary>
        /// The reward tier identifier.
        /// </summary>
        public string TierId { get; set; }

        /// <summary>
        /// Type of the profile - for example Standard, Google etc.
        /// </summary>
        public IList<LoginProvider> LoginProviders { get; set; }

        /// <summary>
        /// Status of the customer profile
        ///
        /// 
        /// </summary>
        public CustomerProfileStatus Status { get; set; }
    }
}
