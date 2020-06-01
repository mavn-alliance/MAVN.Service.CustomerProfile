using System;
using JetBrains.Annotations;

namespace MAVN.Service.CustomerProfile.Client.Models.Responses
{
    /// <summary>
    /// Holds payment provider details
    /// </summary>
    [PublicAPI]
    public class PaymentProviderDetails
    {
        /// <summary>
        /// Id of the details
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Id of the partner
        /// </summary>
        public Guid PartnerId { get; set; }

        /// <summary>
        /// Name of the payment provider
        /// </summary>
        public string PaymentIntegrationProvider { get; set; }

        /// <summary>
        /// Payment integration properties for the provider
        /// </summary>
        public string PaymentIntegrationProperties { get; set; }
    }
}
