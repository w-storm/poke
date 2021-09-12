using NUnit.Framework;
using System.Net;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Poke.Api.Controllers;
using Poke.Models;
using Poke.Services;

namespace Poke.Api.Tests
{
    [TestFixture]
    public class PokemonControllerTests
    {
        private Mock<IPokemonService> _mockPokemonService;

        [SetUp]
        public void Setup()
        {
            _mockPokemonService = new Mock<IPokemonService>();

            _mockPokemonService.Setup(i => i.Get(It.Is<string>(p => p == "xxx"))).ReturnsAsync(new PokemonDetails
                {
                    Name = "xxx"
                });

            _mockPokemonService.Setup(i => i.GetTranslated(It.Is<string>(p => p == "xxx"))).ReturnsAsync(new PokemonDetails
                {
                    Name = "xxx"
                });
        }

        [Test]

        public async Task Get_Should_Return_Ok_With_PokemonDetails()
        {
            var controller = new PokemonController(_mockPokemonService.Object);

            var result = await controller.Get("xxx");

            ((ObjectResult)result).StatusCode.Should().Be((int)HttpStatusCode.OK);

            var objectResult = result as OkObjectResult;
            objectResult.Should().NotBeNull();

            var pokemon = objectResult.Value as PokemonDetails;

            pokemon.Should().NotBeNull();
            pokemon.Name.Should().Be("xxx");
        }

        [Test]

        public async Task GetTranslated_Should_Return_Ok_With_PokemonDetails()
        {
            var controller = new PokemonController(_mockPokemonService.Object);

            var result = await controller.GetTranslated("xxx");

            ((ObjectResult)result).StatusCode.Should().Be((int)HttpStatusCode.OK);

            var objectResult = result as OkObjectResult;
            objectResult.Should().NotBeNull();

            var pokemon = objectResult.Value as PokemonDetails;

            pokemon.Should().NotBeNull();
            pokemon.Name.Should().Be("xxx");
        }
    }
}
