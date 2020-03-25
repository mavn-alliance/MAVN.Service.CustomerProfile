using System.ComponentModel.DataAnnotations;
using Lykke.Service.CustomerProfile.Client.Models.Constants;

namespace Lykke.Service.CustomerProfile.Client.Models.Requests
{
    /// <summary>
    /// Email update request model.
    /// </summary>
    public class EmailUpdateRequestModel
    {
        /// <summary>Customer id</summary>
        public string CustomerId { get; set; }

        /// <summary>Email</summary>
        [RegularExpression(ValidationConstants.EmailValidationPattern)]
        public string Email { get; set; }
    }
}
