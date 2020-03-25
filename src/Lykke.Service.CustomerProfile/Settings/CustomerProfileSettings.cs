using JetBrains.Annotations;

namespace Lykke.Service.CustomerProfile.Settings
{
    [UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
    public class CustomerProfileSettings
    {
        public DbSettings Db { get; set; }

        public RabbitMqSettings RabbitMq { get; set; }
    }
}
