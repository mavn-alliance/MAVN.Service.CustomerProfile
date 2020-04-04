using System;
using System.Threading.Tasks;
using JetBrains.Annotations;
using MAVN.Service.CustomerProfile.Client.Models.Responses;
using Refit;

namespace MAVN.Service.CustomerProfile.Client.Api
{
    /// <summary>
    /// Provides methods for work with customer statistics.
    /// </summary>
    [PublicAPI]
    public interface IStatisticsApi
    {
        /// <summary>
        /// Returns information about customers registrations by the period of time.
        /// </summary>
        /// <param name="startDate">The start date of period.</param>
        /// <param name="endDate">The end date of period.</param>
        /// <returns>The information about customers registrations.</returns>
        [Get("/api/statistics")]
        Task<CustomerStatisticsResponse> GetByPeriodAsync(DateTime startDate, DateTime endDate);
    }
}
