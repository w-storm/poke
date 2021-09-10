using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace Poke.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class PokemonController : ControllerBase
    {


        [HttpGet, Route("{name}")]
        public async Task<IActionResult> Get(string name)
        {

            return new OkObjectResult("xxx");

        }

        [HttpGet, Route("translated/{name}")]
        public async Task<IActionResult> GetTranslated(string name)
        {

            return Ok("translated");

        }
    }
}
