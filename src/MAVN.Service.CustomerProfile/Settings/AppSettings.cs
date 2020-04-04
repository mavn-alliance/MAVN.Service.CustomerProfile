using JetBrains.Annotations;
using Lykke.Sdk.Settings;
using Lykke.Service.Dictionaries.Client;

namespace MAVN.Service.CustomerProfile.Settings
{
    [UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
    public class AppSettings : BaseAppSettings
    {
        public CustomerProfileSettings CustomerProfileService { get; set; }
        
        public DictionariesServiceClientSettings DictionariesServiceClient { get; set; }
    }
}
