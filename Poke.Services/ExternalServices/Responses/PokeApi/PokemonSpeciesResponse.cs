using System.Collections.Generic;

namespace Poke.Services.ExternalServices.Responses.PokeApi
{
    public class PokemonSpeciesResponse
    {
        public string Name { get; set; }
        public bool Is_Legendary { get; set; }
        public Habitat Habitat { get; set; }
        public List<FlavorTextEntry> flavor_text_entries { get; set; }
    }
}
