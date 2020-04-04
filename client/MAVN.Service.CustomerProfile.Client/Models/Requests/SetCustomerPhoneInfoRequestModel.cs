using System.ComponentModel.DataAnnotations;
using MAVN.Service.CustomerProfile.Client.Models.Constants;

namespace MAVN.Service.CustomerProfile.Client.Models.Requests
{
    /// <summary>
    /// Request model to set phone info for a customer
    /// </summary>
    public class SetCustomerPhoneInfoRequestModel
    {
        /// <summary>
        /// Id of the customer
        /// </summary>
        [Required]
        public string CustomerId { get; set; }

        /// <summary>
        /// Phone number of the customer
        /// </summary>
        [Required]
        [RegularExpression(ValidationConstants.PhoneValidationPattern)]
        [MaxLength(15)]
        public string PhoneNumber { get; set; }

        /// <summary>
        /// Country phone code id (optional)
        /// </summary>
        [Required]
        public int CountryPhoneCodeId { get; set; }
    }
}
