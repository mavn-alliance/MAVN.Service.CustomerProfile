using Lykke.SettingsReader.Attributes;

namespace Lykke.Service.CustomerProfile.Settings
{
    public class RabbitMqSettings
    {
        [AmqpCheck]
        public string RabbitMqConnectionString { get; set; }
    }
}
