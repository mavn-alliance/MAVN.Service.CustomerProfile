using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Common.Log;
using Lykke.Common;
using Lykke.Common.Log;
using Lykke.Sdk;

namespace MAVN.Service.CustomerProfile.DomainServices
{
    public class StartupManager : IStartupManager
    {
        private readonly ILog _log;
        private readonly IEnumerable<IStartStop> _startables;
        private readonly string _encryptionKey;
        private readonly string _encryptionIv;

        public StartupManager(
            ILogFactory logFactory,
            string encryptionKey,
            string encryptionIv,
            IEnumerable<IStartStop> startables)
        {
            _encryptionKey = encryptionKey;
            _encryptionIv = encryptionIv;
            _startables = startables;
            _log = logFactory?.CreateLog(this) ?? throw new ArgumentNullException(nameof(logFactory));
        }

        public Task StartAsync()
        {
            _log.Info("Checking encryption keys ...");

            if (string.IsNullOrEmpty(_encryptionKey))
                _log.Warning("Encryption key is empty. Make sure to set it up before using the application.");

            if (string.IsNullOrEmpty(_encryptionIv))
            {
                _log.Error(message: "Encryption IV is empty.");
                
                throw new ArgumentNullException(nameof(_encryptionIv));
            }

            _log.Info("Encryption keys check completed.");

            foreach (var item in _startables)
            {
                try
                {
                    item.Start();
                }
                catch (Exception e)
                {
                    _log.Error(e);
                }
            }

            return Task.CompletedTask;
        }
    }
}
