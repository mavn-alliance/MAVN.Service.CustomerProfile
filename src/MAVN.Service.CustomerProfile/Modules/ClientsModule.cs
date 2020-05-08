﻿using Autofac;
using JetBrains.Annotations;
using MAVN.Service.CustomerProfile.Settings;
using MAVN.Service.Dictionaries.Client;
using Lykke.SettingsReader;

namespace MAVN.Service.CustomerProfile.Modules
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
