using Microsoft.Extensions.Configuration;
using System;

namespace Poke.Models
{
    public interface IConfig
    {
        string ExternalServicesPokeApiUrl { get; }
        int ExternalServicesPokeApiTimeoutSeconds { get; }

        string ExternalServicesFunTranslationsUrl { get; }
        int ExternalServicesFunTranslationsTimeoutSeconds { get; }
    }

    public class Config : ConfigBase
    {
        private readonly IConfigurationRoot _configurationRoot;

        public Config(IConfigurationRoot configurationRoot)
        {
            _configurationRoot = configurationRoot ?? throw new ArgumentNullException(nameof(configurationRoot));
        }

        public override string ExternalServicesPokeApiUrl => (_configurationRoot["ExternalServices:PokeApi:Url"]);
        public override int ExternalServicesPokeApiTimeoutSeconds => (int.Parse(_configurationRoot["ExternalServices:PokeApi:timeoutSeconds"]));

        public override string ExternalServicesFunTranslationsUrl => (_configurationRoot["ExternalServices:FunTranslations:Url"]);
        public override int ExternalServicesFunTranslationsTimeoutSeconds => (int.Parse(_configurationRoot["ExternalServices:FunTranslations:timeoutSeconds"]));

    }

    public class ConfigBase : IConfig
    {
        public virtual string ExternalServicesPokeApiUrl { get; }
        public virtual int ExternalServicesPokeApiTimeoutSeconds { get; }
        public virtual string ExternalServicesFunTranslationsUrl { get; }
        public virtual int ExternalServicesFunTranslationsTimeoutSeconds { get; }
    }
}
