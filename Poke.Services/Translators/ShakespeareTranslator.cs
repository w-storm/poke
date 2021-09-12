using System.Threading.Tasks;
using Poke.Models;
using Poke.Services.ExternalServices;

namespace Poke.Services.Translators
{
    public class ShakespeareTranslator: ITranslator, IShakespeareTranslator
    {
        private const string NotHabitat = "cave";
        private const bool IsLegendary = false;

        private readonly IFunTranslationsApiClient _funTranslationsApiClient;

        public ShakespeareTranslator(IFunTranslationsApiClient funTranslationsApiClient)
        {
            _funTranslationsApiClient = funTranslationsApiClient;
        }

        public bool CanTranslate(PokemonDetails pokemonDetails)
        {
            return pokemonDetails is { IsLegendary: IsLegendary } 
                   && pokemonDetails.Habitat != NotHabitat;
        }

        public async Task<string> Translate(string text)
        {
            return await _funTranslationsApiClient.TranslateToShakespeare(text);
        }
    }
}
