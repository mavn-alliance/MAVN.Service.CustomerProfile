using System.ComponentModel.DataAnnotations;
using JetBrains.Annotations;
using MAVN.Service.CustomerProfile.Client.Models.Constants;

namespace MAVN.Service.CustomerProfile.Client.Models.Requests
{
    /// <summary>
    /// Holds the data with which the contact needs to be created
    /// </summary>
    [PublicAPI]
    public class PartnerContactRequestModel
    {
        /// <summary>
        /// Internal LocationId used to identify Contact person between different components/microservices
        /// </summary>
        [Required]
        public string LocationId { get; set; }

        /// <summary>
        /// The Email of the Contact person
        /// </summary>
        [DataType(DataType.EmailAddress)]
        [MaxLength(100)]
        public string Email { get; set; }

        /// <summary>
        /// Contact First name
        /// </summary>
        [MaxLength(100)]
        public string FirstName { get; set; }

        /// <summary>
        /// Contact Last name
        /// </summary>
        [MaxLength(100)]
        public string LastName { get; set; }

        /// <summary>
        /// Contact Phone number
        /// </summary>
        [MaxLength(50)]
        public string PhoneNumber { get; set; }
    }
}
