using Lykke.SettingsReader.Attributes;

namespace MAVN.Service.CustomerProfile.Settings
{
    public class DbSettings
    {
        [AzureTableCheck]
        public string LogsConnString { get; set; }
        public string DataConnectionString { get; set; }
    }
}
