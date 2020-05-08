using System;
using Autofac;
using MAVN.Common.Encryption;
using JetBrains.Annotations;
using Lykke.Sdk;
using MAVN.Service.CustomerProfile.Domain.Services;
using MAVN.Service.CustomerProfile.DomainServices;
using MAVN.Service.CustomerProfile.Settings;
using Lykke.SettingsReader;

namespace MAVN.Service.CustomerProfile.Modules
{
    [UsedImplicitly]
    public class ServiceModule : Module
    {
        private readonly IReloadingManager<AppSettings> _appSettings;

        public ServiceModule(IReloadingManager<AppSettings> appSettings)
        {
            _appSettings = appSettings;
        }

        protected override void Load(ContainerBuilder builder)
        {
            var encryptionKey = Environment.GetEnvironmentVariable("EncryptionKey");
            var encryptionIv = Environment.GetEnvironmentVariable("EncryptionIV");

            builder.RegisterInstance(new AesSerializer(encryptionKey, encryptionIv))
                .As<IAesSerializer>()
                .SingleInstance();

            builder.RegisterType<EncryptionService>()
                .As<IEncryptionService>()
                .SingleInstance();

            builder.RegisterType<AdminProfileService>()
                .As<IAdminProfileService>()
                .SingleInstance();

            builder.RegisterType<CustomerProfileService>()
                .As<ICustomerProfileService>()
                .SingleInstance();

            builder.RegisterType<PartnerContactService>()
                .As<IPartnerContactService>()
                .SingleInstance();

            builder.RegisterType<ReferralHotelProfileService>()
                .As<IReferralHotelProfileService>()
                .SingleInstance();

            builder.RegisterType<ReferralLeadProfileService>()
                .As<IReferralLeadProfileService>()
                .SingleInstance();

            builder.RegisterType<ReferralFriendProfileService>()
                .As<IReferralFriendProfileService>()
                .SingleInstance();

            builder.RegisterType<StatisticsService>()
                .As<IStatisticsService>()
                .SingleInstance();

            var apiKeysPairs = Environment.GetEnvironmentVariable("CPApiKeysPairs");
            builder.RegisterType<ApiKeyService>()
                .As<IApiKeyService>()
                .SingleInstance()
                .WithParameter(TypedParameter.From(apiKeysPairs));

            builder.RegisterType<StartupManager>()
                .WithParameter("encryptionKey", encryptionKey)
                .WithParameter("encryptionIv", encryptionIv)
                .As<IStartupManager>()
                .SingleInstance();

            builder.RegisterType<ShutdownManager>()
                .As<IShutdownManager>()
                .SingleInstance()
                .AutoActivate();
        }
    }
}
