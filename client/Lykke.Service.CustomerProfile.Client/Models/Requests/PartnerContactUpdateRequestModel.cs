using System.ComponentModel.DataAnnotations;
using JetBrains.Annotations;
using Lykke.Service.CustomerProfile.Client.Models.Constants;

namespace Lykke.Service.CustomerProfile.Client.Models.Requests
{
    /// <summary>
    /// Represents a partner contact data.
    /// </summary>
    [PublicAPI]
    public class PartnerContactUpdateRequestModel
    {
        /// <summary>
        /// The location identifier.
        /// </summary>
        [Required]
        public string LocationId { get; set; }

        /// <summary>
        /// The contact first name.
        /// </summary>
        [Required]
        [MaxLength(100)]
        public string FirstName { get; set; }

        /// <summary>
        /// The contact last name.
        /// </summary>
        [Required]
        [MaxLength(100)]
        public string LastName { get; set; }

        /// <summary>
        /// The contact phone number.
        /// </summary>
        [Required]
        [MaxLength(50)]
        public string PhoneNumber { get; set; }

        /// <summary>
        /// The contact phone number.
        /// </summary>
        [Required, DataType(DataType.EmailAddress)]
        [RegularExpression(ValidationConstants.EmailValidationPattern)]
        [MaxLength(100)]
        public string Email { get; set; }
    }
}
