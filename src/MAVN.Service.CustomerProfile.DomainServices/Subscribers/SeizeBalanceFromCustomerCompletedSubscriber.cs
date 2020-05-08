using System.Threading.Tasks;
using Common.Log;
using Lykke.Common.Log;
using Lykke.RabbitMqBroker.Subscriber;
using MAVN.Service.CustomerProfile.Domain.Services;
using MAVN.Service.PrivateBlockchainFacade.Contract.Events;

namespace MAVN.Service.CustomerProfile.DomainServices.Subscribers
{
    public class SeizeBalanceFromCustomerCompletedSubscriber : JsonRabbitSubscriber<SeizeBalanceFromCustomerCompletedEvent>
    {
        private const string ExchangeName = "lykke.wallet.seizebalancefromcustomercompleted";
        private const string QueueName = "customerprofile";

        private readonly ICustomerProfileService _customerProfileService;
        private readonly ILog _log;

        public SeizeBalanceFromCustomerCompletedSubscriber(
            string connectionString,
            ICustomerProfileService customerProfileService,
            ILogFactory logFactory)
            : base(connectionString, ExchangeName, QueueName, logFactory)
        {
            _customerProfileService = customerProfileService;
            _log = logFactory.CreateLog(this);
        }

        protected override async Task ProcessMessageAsync(SeizeBalanceFromCustomerCompletedEvent message)
        {
            await _customerProfileService.MarkCustomerAsDeactivated(message.CustomerId);

            _log.Info("Processed SeizedFromCustomerEvent event", message);
        }
    }
}
