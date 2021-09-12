using System.Threading.Tasks;
using Poke.Services.ExternalServices.Responses.PokeApi;

namespace Poke.Services.ExternalServices
{
    public interface IPokeApiClient
    {
        Task<PokemonSpeciesResponse> GetPokemonSpecies(string name);
    }
}