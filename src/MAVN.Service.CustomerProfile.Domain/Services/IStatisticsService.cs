using System;
using System.Threading.Tasks;
using MAVN.Service.CustomerProfile.Domain.Models;

namespace MAVN.Service.CustomerProfile.Domain.Services
{
    public interface IStatisticsService
    {
        Task<StatisticsResult> GetByPeriodAsync(DateTime startDate, DateTime endDate);
    }
}
