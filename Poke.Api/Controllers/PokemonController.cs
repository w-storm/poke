using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Poke.Services;

namespace Poke.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class PokemonController : ControllerBase
    {
        private readonly IPokemonService _pokemonService;

        public PokemonController(
            IPokemonService pokemonService)
        {
            _pokemonService = pokemonService;
        }

        [HttpGet, Route("{name}")]
        public async Task<IActionResult> Get(string name)
        {
            var details = await _pokemonService.Get(name);

            return new OkObjectResult(details);
        }

        [HttpGet, Route("translated/{name}")]
        public async Task<IActionResult> GetTranslated(string name)
        {
            var details = await _pokemonService.GetTranslated(name);

            return new OkObjectResult(details);
        }
    }
}
