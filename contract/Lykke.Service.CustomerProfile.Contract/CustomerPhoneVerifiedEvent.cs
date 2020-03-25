using System;

namespace Lykke.Service.CustomerProfile.Contract
{
    public class CustomerPhoneVerifiedEvent
    {
        /// <summary>
        /// Id of the customer
        /// </summary>
        public string CustomerId { get; set; }

        /// <summary>
        /// Indicates whether this is the first time when the customer verifies his phone
        /// </summary>
        public bool WasPhoneEverVerified { get; set; }

        /// <summary>
        /// Timestamp of the event
        /// </summary>
        public DateTime Timestamp { get; set; }
    }
}
