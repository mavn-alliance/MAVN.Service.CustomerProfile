using Autofac;
using JetBrains.Annotations;
using Lykke.Common;
using Lykke.RabbitMqBroker.Publisher;
using MAVN.Service.CustomerProfile.Contract;
using MAVN.Service.CustomerProfile.DomainServices.Subscribers;
using MAVN.Service.CustomerProfile.Settings;
using Lykke.SettingsReader;

namespace MAVN.Service.CustomerProfile.Modules
{
    [UsedImplicitly]
    public class RabbitMqModule : Module
    {
        private const string CustomerEmailVerifiedExchangeName = "lykke.customer.emailverified";
        private const string CustomerPhoneVerifiedExchangeName = "lykke.customer.phoneverified";
        private const string CustomerProfileDeactivationRequestedExchangeName = "lykke.customer.profiledeactivationrequested";


        private readonly string _connString;

        public RabbitMqModule(IReloadingManager<AppSettings> appSettings)
        {
            _connString = appSettings.CurrentValue.CustomerProfileService.RabbitMq.RabbitMqConnectionString;
        }

        protected override void Load(ContainerBuilder builder)
        {
            #region Customer

            builder.RegisterType<CodeVerifiedSubscriber>()
                .As<IStartStop>()
                .SingleInstance()
                .WithParameter(TypedParameter.From(_connString));

            builder.RegisterType<CustomerTierChangedSubscriber>()
                .As<IStartStop>()
                .SingleInstance()
                .WithParameter(TypedParameter.From(_connString));

            builder.RegisterType<SeizeBalanceFromCustomerCompletedSubscriber>()
                .As<IStartStop>()
                .SingleInstance()
                .WithParameter(TypedParameter.From(_connString));

            builder.RegisterJsonRabbitPublisher<EmailVerifiedEvent>(
                _connString,
                CustomerEmailVerifiedExchangeName);

            builder.RegisterJsonRabbitPublisher<CustomerPhoneVerifiedEvent>(
                _connString,
                CustomerPhoneVerifiedExchangeName);

            builder.RegisterJsonRabbitPublisher<CustomerProfileDeactivationRequestedEvent>(
                _connString,
                CustomerProfileDeactivationRequestedExchangeName);

            #endregion

            #region Admin

            builder.RegisterType<AdminEmailVerifiedSubscriber>()
                .As<IStartStop>()
                .SingleInstance()
                .WithParameter(TypedParameter.From(_connString));

            #endregion
        }
    }
}
