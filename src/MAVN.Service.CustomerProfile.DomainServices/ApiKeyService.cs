using System;
using System.Collections.Generic;
using Common.Log;
using Lykke.Common.Log;
using MAVN.Service.CustomerProfile.Domain.Services;

namespace MAVN.Service.CustomerProfile.DomainServices
{
    /// <summary>
    /// Validator for auth api key
    /// </summary>
    public class ApiKeyService : IApiKeyService
    {
        private readonly Dictionary<string, string> _apiKeys;
        private readonly ILog _log;

        public ApiKeyService(string apiKeysStr, ILogFactory logFactory)
        {
            _log = logFactory.CreateLog(this);

            if (!string.IsNullOrWhiteSpace(apiKeysStr))
            {
                var apiKeyParts = apiKeysStr.Trim().Split('|');
                if (apiKeyParts.Length % 2 != 0)
                    throw new InvalidOperationException("Api keys env var has inconsistent value");

                _apiKeys = new Dictionary<string, string>(apiKeyParts.Length / 2);
                for (int i = 0; i < apiKeyParts.Length; i += 2)
                {
                    // Reversing key-value relation for faster key search
                    _apiKeys.Add(apiKeyParts[i + 1].Trim(), apiKeyParts[i].Trim());
                }
            }

            if (_apiKeys.Count == 0)
                _log.Warning("No api keys were set.");
        }

        public bool ValidateKey(string apiKey)
        {
            return _apiKeys.ContainsKey(apiKey);
        }

        public string GetKeyName(string apiKey)
        {
            _apiKeys.TryGetValue(apiKey, out var apiKeyName);

            return apiKeyName;
        }
    }
}

