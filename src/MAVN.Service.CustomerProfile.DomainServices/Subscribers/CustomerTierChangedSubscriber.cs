using System.Threading.Tasks;
using Common.Log;
using Lykke.Common.Log;
using Lykke.RabbitMqBroker.Subscriber;
using MAVN.Service.CustomerProfile.Domain.Services;
using Lykke.Service.Tiers.Contract;

namespace MAVN.Service.CustomerProfile.DomainServices.Subscribers
{
    public class CustomerTierChangedSubscriber : JsonRabbitSubscriber<CustomerTierChangedEvent>
    {
        private const string ExchangeName = "lykke.bonus.customertierchanged";
        private const string QueueName = "customerprofile";

        private readonly ICustomerProfileService _customerProfileService;
        private readonly ILog _log;

        public CustomerTierChangedSubscriber(
            string connectionString,
            ICustomerProfileService customerProfileService,
            ILogFactory logFactory)
            : base(connectionString, ExchangeName, QueueName, logFactory)
        {
            _customerProfileService = customerProfileService;
            _log = logFactory.CreateLog(this);
        }

        protected override async Task ProcessMessageAsync(CustomerTierChangedEvent message)
        {
            await _customerProfileService.UpdateTierAsync(message.CustomerId.ToString(), message.TierId.ToString());

            _log.Info("Processed customer tier changed event",
                context: $"customerId: {message.CustomerId}; tierId: {message.TierId}");
        }
    }
}
