using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Poke.Api.Controllers;
using Poke.Api.Models;

namespace Poke.Api.Tests
{
    [TestFixture]
    public class PokemonControllerTests
    {


        [Test]

        public async Task Get_Should_Return_Ok()
        {
            var controller = new PokemonController();

            var result = await controller.Get("xxx");

            ((ObjectResult)result).StatusCode.Should().Be((int)HttpStatusCode.OK);

            var objectResult = result as OkObjectResult;
            objectResult.Should().NotBeNull();

            var pokemon = objectResult.Value as PokemonDetailsResponse;

            pokemon.Should().NotBeNull();
            pokemon.Name.Should().Be("xxx");
        }

        [Test]

        public async Task GetTranslated_Should_Return_Ok()
        {
            var controller = new PokemonController();

            var result = await controller.GetTranslated("xxx");

            ((ObjectResult)result).StatusCode.Should().Be((int)HttpStatusCode.OK);

            var objectResult = result as OkObjectResult;
            objectResult.Should().NotBeNull();

            var pokemon = objectResult.Value as PokemonDetailsResponse;

            pokemon.Should().NotBeNull();
            pokemon.Name.Should().Be("xxx");
        }
    }
}
