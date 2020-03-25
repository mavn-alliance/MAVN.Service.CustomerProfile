using System;

namespace Lykke.Service.CustomerProfile.Contract
{
    /// <summary>
    /// Represents a Customer Email verification event
    /// </summary>
    public class EmailVerifiedEvent
    {
        /// <summary>
        /// Represents Falcon's CustomerId
        /// </summary>
        public string CustomerId { get; set; }

        /// <summary>
        /// Represents timeStamp of Email verification
        /// </summary>
        public DateTime TimeStamp { get; set; }

        /// <summary>
        /// Tells if email has ever been verified before
        /// </summary>
        public bool WasEmailEverVerified { get; set; }
    }
}
