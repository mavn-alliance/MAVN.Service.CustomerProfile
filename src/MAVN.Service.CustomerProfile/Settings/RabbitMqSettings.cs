using Lykke.SettingsReader.Attributes;

namespace MAVN.Service.CustomerProfile.Settings
{
    public class RabbitMqSettings
    {
        [AmqpCheck]
        public string RabbitMqConnectionString { get; set; }
    }
}
