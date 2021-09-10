using Poke.Models;
using System;
using System.Threading.Tasks;

namespace Poke.Services
{
    public class PokemonService : IPokemonService
    {
        public async Task<PokemonDetails> Get(string name)
        {
            return new PokemonDetails();
        }

        public async Task<PokemonDetails> GetTranslated(string name)
        {
            return new PokemonDetails();
        }
    }
}
