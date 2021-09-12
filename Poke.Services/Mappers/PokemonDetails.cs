using System.Linq;
using Poke.Services.ExternalServices.Responses.PokeApi;

namespace Poke.Services.Mappers
{
    public static class PokemonDetail
    {
        public static Models.PokemonDetails FromPokemonSpeciesResponse(this PokemonSpeciesResponse pokemonSpeciesResponse)
        {
            if (pokemonSpeciesResponse == null)
            {
                return null;
            }

            var result = new Models.PokemonDetails
                   {
                       Name = pokemonSpeciesResponse.Name,
                       Habitat = pokemonSpeciesResponse.Habitat?.Name,
                       IsLegendary = pokemonSpeciesResponse.Is_Legendary,
                       Description = pokemonSpeciesResponse.flavor_text_entries?.FirstOrDefault(i => i.Language.Name == "en")
                           ?.flavor_text.Replace("\\n", string.Empty)
                   };

            return result;
        }
    }
}
