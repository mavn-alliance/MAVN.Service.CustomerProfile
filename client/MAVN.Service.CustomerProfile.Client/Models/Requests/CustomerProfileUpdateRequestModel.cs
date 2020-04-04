using System.ComponentModel.DataAnnotations;
using JetBrains.Annotations;
using MAVN.Service.CustomerProfile.Client.Models.Constants;

namespace MAVN.Service.CustomerProfile.Client.Models.Requests
{
    /// <summary>
    /// Represents a customer KYA data.
    /// </summary>
    [PublicAPI]
    public class CustomerProfileUpdateRequestModel
    {
        /// <summary>
        /// The customer identifier.
        /// </summary>
        [Required]
        public string CustomerId { get; set; }

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
        /// The customer phone number.
        /// </summary>
        [Required]
        [MaxLength(15)]
        [RegularExpression(ValidationConstants.PhoneValidationPattern)]
        public string PhoneNumber { get; set; }

        /// <summary>
        /// The phone country dialing code identifier.
        /// </summary>
        [Required]
        public int CountryPhoneCodeId { get; set; }

        /// <summary>
        /// The country of residence identifier.
        /// </summary>
        [Required]
        public int CountryOfResidenceId { get; set; }
    }
}
