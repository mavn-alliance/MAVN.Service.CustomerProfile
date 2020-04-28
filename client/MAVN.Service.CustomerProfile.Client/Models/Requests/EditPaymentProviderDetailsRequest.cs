using System;
using System.ComponentModel.DataAnnotations;
using JetBrains.Annotations;

namespace MAVN.Service.CustomerProfile.Client.Models.Requests
{
    /// <summary>
    /// Edit request model
    /// </summary>
    [PublicAPI]
    public class EditPaymentProviderDetailsRequest : CreatePaymentProviderDetailsRequest
    {
        /// <summary>
        /// Id of the details model
        /// </summary>
        [Required]
        public Guid Id { get; set; }
    }
}
