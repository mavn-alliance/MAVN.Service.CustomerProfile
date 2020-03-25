using System;
using System.Threading.Tasks;
using Lykke.Service.CustomerProfile.Domain.Models;

namespace Lykke.Service.CustomerProfile.Domain.Services
{
    public interface IStatisticsService
    {
        Task<StatisticsResult> GetByPeriodAsync(DateTime startDate, DateTime endDate);
    }
}
