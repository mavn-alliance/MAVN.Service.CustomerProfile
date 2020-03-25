using JetBrains.Annotations;

namespace Lykke.Service.CustomerProfile.Client.Models.Responses
{
    /// <summary>
    /// Represents a customers statistics.
    /// </summary>
    [PublicAPI]
    public class CustomerStatisticsResponse
    {
        /// <summary>
        /// The number of customers that registered in a specific period of time. 
        /// </summary>
        public int RegistrationsCount { get; set; }

        /// <summary>
        /// The number of all customers.
        /// </summary>
        public int TotalCount { get; set; }
    }
}
