using FluentAssertions;
using Moq;
using NUnit.Framework;
using Poke.Models;
using Poke.Services.ExternalServices;
using Poke.Services.Translators;

namespace Poke.Services.Tests.Translators
{
    [TestFixture]
    public class ShakespeareTranslatorTests
    {
        private Mock<IFunTranslationsApiClient> _mockFunTranslationsApiClient;

        [SetUp]
        public void Setup()
        {
            _mockFunTranslationsApiClient = new Mock<IFunTranslationsApiClient>();
        }

        [TestCase("cave", false, false)]
        [TestCase("sea", true, false)]
        [TestCase("sea", false, true)]
        public void CanTranslate_Should_Return_Expected_Responses(string habitat, bool isLegendary, bool expectedResult)
        {
            var sut = new ShakespeareTranslator(_mockFunTranslationsApiClient.Object);

            var pokemon = new PokemonDetails
                          {
                              Habitat = habitat,
                              IsLegendary = isLegendary
                          };

            var result = sut.CanTranslate(pokemon);

            result.Should().Be(expectedResult);
        }
    }


}
