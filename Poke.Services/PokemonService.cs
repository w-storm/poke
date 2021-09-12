using Poke.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Poke.Services.ExternalServices;
using Poke.Services.Translators;
using Poke.Services.Mappers;

namespace Poke.Services
{
    public class PokemonService : IPokemonService
    {
        private readonly IPokeApiClient _pokeApiClient;
        private readonly List<ITranslator> _translators;

        public PokemonService(
            IPokeApiClient pokeApiClient,
            IShakespeareTranslator shakespeareTranslator,
            IYodaTranslator yodaTranslator)
        {
            _pokeApiClient = pokeApiClient;

            _translators = new List<ITranslator>
            {
                (ITranslator)shakespeareTranslator,
                (ITranslator)yodaTranslator
            };
        }

        public async Task<PokemonDetails> Get(string name)
        {
            var pokemon = await _pokeApiClient.GetPokemonSpecies(name);

            return pokemon.FromPokemonSpeciesResponse();
        }

        public async Task<PokemonDetails> GetTranslated(string name)
        {
            var pokemon = await _pokeApiClient.GetPokemonSpecies(name);
            var pokemonDetails = pokemon.FromPokemonSpeciesResponse();

            try
            {
                foreach (var translator in _translators.Where(translator => translator.CanTranslate(pokemonDetails)))
                {
                    var translated = await translator.Translate(pokemonDetails.Description);
                    pokemonDetails.Description = translated;
                    return pokemonDetails;
                }
            }
            catch (Exception exception)
            {
                //log the issue and continue to return the standard description
            }
           
            // or if not suitable translator was found, return the original

            return pokemonDetails;
        }
    }
}
