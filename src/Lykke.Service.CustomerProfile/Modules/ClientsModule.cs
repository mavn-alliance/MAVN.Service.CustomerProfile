using Autofac;
using JetBrains.Annotations;
using Lykke.Service.CustomerProfile.Settings;
using Lykke.Service.Dictionaries.Client;
using Lykke.SettingsReader;

namespace Lykke.Service.CustomerProfile.Modules
{
    [UsedImplicitly]
    public class ClientsModule : Module
    {
        private readonly AppSettings _apiSettings;

        public ClientsModule(IReloadingManager<AppSettings> settings)
        {
            _apiSettings = settings.CurrentValue;
        }

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterDictionariesClient(_apiSettings.DictionariesServiceClient);
        }
    }
}
