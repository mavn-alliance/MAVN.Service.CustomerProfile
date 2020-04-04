using System;
using System.Net;
using System.Threading.Tasks;
using MAVN.Service.CustomerProfile.Client.Api;
using MAVN.Service.CustomerProfile.Client.Models.Responses;
using MAVN.Service.CustomerProfile.Domain.Exceptions;
using MAVN.Service.CustomerProfile.Domain.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MAVN.Service.CustomerProfile.Controllers
{
    [Authorize]
    [ApiController]
    [Route("/api/statistics")]
    public class StatisticsController : ControllerBase, IStatisticsApi
    {
        private readonly IStatisticsService _statisticsService;

        public StatisticsController(IStatisticsService statisticsService)
        {
            _statisticsService = statisticsService;
        }

        /// <summary>
        /// Returns information about customers registrations by the period of time.
        /// </summary>
        /// <param name="startDate">The start date of period.</param>
        /// <param name="endDate">The end date of period.</param>
        /// <returns>The information about customers registrations.</returns>
        /// <response code="200">The information about customers registrations.</response>
        [HttpGet]
        [ProducesResponseType(typeof(CustomerStatisticsResponse), (int) HttpStatusCode.OK)]
        public async Task<CustomerStatisticsResponse> GetByPeriodAsync(DateTime startDate, DateTime endDate)
        {
            if (startDate >= endDate)
                throw new BadRequestException($"{nameof(startDate)} must be earlier than {nameof(endDate)}");

            var statistics = await _statisticsService.GetByPeriodAsync(startDate, endDate);

            return new CustomerStatisticsResponse
            {
                RegistrationsCount = statistics.RegistrationsCount,
                TotalCount = statistics.TotalCount,
            };
        }
    }
}
