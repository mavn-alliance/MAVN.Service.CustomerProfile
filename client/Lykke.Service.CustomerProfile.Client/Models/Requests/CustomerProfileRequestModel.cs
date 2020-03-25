﻿using System.ComponentModel.DataAnnotations;
using JetBrains.Annotations;
using Lykke.Service.CustomerProfile.Client.Models.Constants;
using Lykke.Service.CustomerProfile.Client.Models.Enums;

namespace Lykke.Service.CustomerProfile.Client.Models.Requests
{
    /// <summary>
    /// Holds the data with which the profile needs to be created
    /// </summary>
    [PublicAPI]
    public class CustomerProfileRequestModel
    {
        /// <summary>
        /// Internal CustomerId used to identify Customers between different components/microservices
        /// </summary>
        [Required]
        public string CustomerId { get; set; }

        /// <summary>
        /// The Email of the Customer
        /// </summary>
        [Required, DataType(DataType.EmailAddress)]
        [RegularExpression(ValidationConstants.EmailValidationPattern)]
        public string Email { get; set; }

        /// <summary>
        /// The customer first name.
        /// </summary>
        [Required]
        [MaxLength(50)]
        [RegularExpression(ValidationConstants.NameValidationPattern)]
        public string FirstName { get; set; }

        /// <summary>
        /// The customer last name.
        /// </summary>
        [Required]
        [MaxLength(50)]
        [RegularExpression(ValidationConstants.NameValidationPattern)]
        public string LastName { get; set; }
        
        /// <summary>
        /// Id of the country of nationality of the customer
        /// </summary>
        public int? CountryOfNationalityId { get; set; }

        /// <summary>
        /// Type of the profile - for example Standard, Google etc.
        /// </summary>
        public LoginProvider LoginProvider { get; set; }
    }
}
