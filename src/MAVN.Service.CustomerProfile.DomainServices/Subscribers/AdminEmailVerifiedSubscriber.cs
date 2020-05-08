using System.Threading.Tasks;
using Common.Log;
using Lykke.Common.Log;
using Lykke.RabbitMqBroker.Subscriber;
using MAVN.Service.AdminManagement.Contract.Events;
using MAVN.Service.CustomerProfile.Domain.Services;

namespace MAVN.Service.CustomerProfile.DomainServices.Subscribers
{
    public class AdminEmailVerifiedSubscriber : JsonRabbitSubscriber<AdminEmailVerifiedEvent>
    {
        private const string ExchangeName = "lykke.admin.emailcodeverified";
        private const string QueueName = "customerprofile";

        private readonly IAdminProfileService _adminProfileService;
        private readonly ILog _log;

        public AdminEmailVerifiedSubscriber(
            string connectionString,
            IAdminProfileService adminProfileService,
            ILogFactory logFactory)
            : base(connectionString, ExchangeName, QueueName, logFactory)
        {
            _adminProfileService = adminProfileService;
            _log = logFactory.CreateLog(this);
        }

        protected override async Task ProcessMessageAsync(AdminEmailVerifiedEvent message)
        {
            await _adminProfileService.SetEmailAsVerifiedAsync(message.AdminId);

            _log.Info($"Processed admin email verified event", message);
        }
    }
}
