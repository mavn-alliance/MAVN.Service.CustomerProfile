using System.ComponentModel.DataAnnotations;

namespace Lykke.Service.CustomerProfile.Client.Models.Requests
{
    /// <summary>
    /// Request to set customer's phone as verified
    /// </summary>
    public class SetPhoneAsVerifiedRequestModel
    {
        /// <summary>
        /// Id of the customer
        /// </summary>
        [Required]
        public string CustomerId { get; set; }
    }
}
