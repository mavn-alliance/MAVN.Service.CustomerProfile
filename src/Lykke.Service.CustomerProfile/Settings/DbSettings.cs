using Lykke.SettingsReader.Attributes;

namespace Lykke.Service.CustomerProfile.Settings
{
    public class DbSettings
    {
        [AzureTableCheck]
        public string LogsConnString { get; set; }
        public string DataConnectionString { get; set; }
    }
}
