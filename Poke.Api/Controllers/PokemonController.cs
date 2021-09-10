using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Poke.Api.Models;

namespace Poke.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class PokemonController : ControllerBase
    {


        [HttpGet, Route("{name}")]
        public async Task<IActionResult> Get(string name)
        {

            var response = new PokemonDetailsResponse
                           {
                               Name = name
                           };

            return new OkObjectResult(response);

        }

        [HttpGet, Route("translated/{name}")]
        public async Task<IActionResult> GetTranslated(string name)
        {
            var response = new PokemonDetailsResponse
                           {
                               Name = name
            };

            return Ok(response);

        }
    }
}
