using System.Threading.Tasks;
using Common.Log;
using Lykke.Common.Log;
using Lykke.RabbitMqBroker.Subscriber;
using Lykke.Service.CustomerManagement.Contract.Events;
using Lykke.Service.CustomerProfile.Domain.Services;

namespace Lykke.Service.CustomerProfile.DomainServices.Subscribers
{
    public class CodeVerifiedSubscriber : JsonRabbitSubscriber<EmailCodeVerifiedEvent>
    {
        private const string ExchangeName = "lykke.customer.emailcodeverified";
        private const string QueueName = "customerprofile";

        private readonly ICustomerProfileService _customerProfileService;
        private readonly ILog _log;

        public CodeVerifiedSubscriber(
            string connectionString,
            ICustomerProfileService customerProfileService,
            ILogFactory logFactory)
            : base(connectionString, ExchangeName, QueueName, logFactory)
        {
            _customerProfileService = customerProfileService;
            _log = logFactory.CreateLog(this);
        }

        protected override async Task ProcessMessageAsync(EmailCodeVerifiedEvent msg)
        {
            await _customerProfileService.SetEmailAsVerifiedAsync(msg.CustomerId);

            _log.Info($"Processed code verified event", msg);
        }
    }
}
