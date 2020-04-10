using Autofac;
using JetBrains.Annotations;
using Lykke.Common.MsSql;
using MAVN.Service.CustomerProfile.Domain.Repositories;
using MAVN.Service.CustomerProfile.MsSqlRepositories;
using MAVN.Service.CustomerProfile.MsSqlRepositories.Repositories;
using MAVN.Service.CustomerProfile.Settings;
using Lykke.SettingsReader;

namespace MAVN.Service.CustomerProfile.Modules
{
    [UsedImplicitly]
    public class DataLayerModule : Module
    {
        private readonly bool _isPhoneVerificationDisabled;
        private readonly string _connectionString;

        public DataLayerModule(IReloadingManager<AppSettings> appSettings)
        {
            _isPhoneVerificationDisabled = appSettings.CurrentValue.CustomerProfileService.IsPhoneVerificationDisabled;
            _connectionString = appSettings.CurrentValue.CustomerProfileService.Db.DataConnectionString;
        }

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterMsSql(
                _connectionString,
                connString => new CustomerProfileContext(connString, false, isPhoneVerificationDisabled: _isPhoneVerificationDisabled),
                dbConn => new CustomerProfileContext(dbConn, isPhoneVerificationDisabled: _isPhoneVerificationDisabled));

            builder.RegisterType<AdminProfileRepository>()
                .As<IAdminProfileRepository>()
                .SingleInstance();

            builder.RegisterType<CustomerProfileRepository>()
                .As<ICustomerProfileRepository>()
                .SingleInstance();

            builder.RegisterType<PartnerContactRepository>()
                .As<IPartnerContactRepository>()
                .SingleInstance();

            builder.RegisterType<ReferralHotelProfileRepository>()
                .As<IReferralHotelProfileRepository>()
                .SingleInstance();

            builder.RegisterType<ReferralLeadProfileRepository>()
                .As<IReferralLeadProfileRepository>()
                .SingleInstance();

            builder.RegisterType<ReferralFriendProfileRepository>()
                .As<IReferralFriendProfileRepository>()
                .SingleInstance();
        }
    }
}
