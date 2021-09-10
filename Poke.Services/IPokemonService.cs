using System.Threading.Tasks;
using Poke.Models;

namespace Poke.Services
{
    public interface IPokemonService
    {
        Task<PokemonDetails> Get(string name);
        Task<PokemonDetails> GetTranslated(string name);
    }
}