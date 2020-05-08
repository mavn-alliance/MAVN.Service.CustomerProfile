using JetBrains.Annotations;
using Lykke.SettingsReader.Attributes;

namespace MAVN.Service.CustomerProfile.Settings
{
    [UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
    public class CustomerProfileSettings
    {
        public DbSettings Db { get; set; }

        public RabbitMqSettings RabbitMq { get; set; }

        [Optional]
        public bool IsPhoneVerificationDisabled { get; set; }
    }
}
