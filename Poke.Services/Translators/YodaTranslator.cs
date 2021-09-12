using System.Threading.Tasks;
using Poke.Models;
using Poke.Services.ExternalServices;

namespace Poke.Services.Translators
{
    public class YodaTranslator : ITranslator, IYodaTranslator
    {
        private const string Habitat = "cave";
        private const bool IsLegendary = true;

        private readonly IFunTranslationsApiClient _funTranslationsApiClient;

        public YodaTranslator(IFunTranslationsApiClient funTranslationsApiClient)
        {
            _funTranslationsApiClient = funTranslationsApiClient;
        }

        public bool CanTranslate(PokemonDetails pokemonDetails)
        {
            return pokemonDetails != null 
                   && (pokemonDetails.IsLegendary == IsLegendary
                       || pokemonDetails.Habitat == Habitat);
        }

        public async Task<string> Translate(string text)
        {
            return await _funTranslationsApiClient.TranslateToYoda(text);
        }
    }
}
