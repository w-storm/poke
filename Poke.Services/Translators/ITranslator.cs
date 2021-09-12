using System.Threading.Tasks;
using Poke.Models;

namespace Poke.Services.Translators
{
    public interface ITranslator
    {
        bool CanTranslate(PokemonDetails pokemonDetails);
        Task<string> Translate(string text);
    }
}