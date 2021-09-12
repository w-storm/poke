using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using Poke.Models.Exceptions;
using Poke.Services.ExternalServices;
using Poke.Services.ExternalServices.Responses;
using Poke.Services.ExternalServices.Responses.PokeApi;
using Poke.Services.Translators;

namespace Poke.Services.Tests
{
    [TestFixture]
    public class PokemonServiceTests
    {
        private Mock<IPokeApiClient> _mockPokeApiClient;
        private IShakespeareTranslator _shakespeareTranslator;
        private IYodaTranslator _yodaTranslator;
        private Mock<IFunTranslationsApiClient> _mockFunTranslationsApiClient;

        [SetUp]
        public void Setup()
        {
            _mockPokeApiClient = new Mock<IPokeApiClient>();
            _mockFunTranslationsApiClient = new Mock<IFunTranslationsApiClient>();
            _shakespeareTranslator = new ShakespeareTranslator(_mockFunTranslationsApiClient.Object);
            _yodaTranslator = new YodaTranslator(_mockFunTranslationsApiClient.Object);
        }

        [Test]
        public async Task Get_Should_Return_Pokemon_Details()
        {
            var name = "xxx";

            var pokemonSpeciesResponse = new PokemonSpeciesResponse
                                         {
                                             Name = "xxx",
                                             flavor_text_entries = new List<FlavorTextEntry>
                                                                   {
                                                                       new FlavorTextEntry
                                                                       {
                                                                           flavor_text = "xxx",
                                                                           Language = new Language
                                                                               {
                                                                                   Name = "en"
                                                                               }
                                                                       }
                                                                   },
                                             Habitat = new Habitat
                                                       {
                                                           Name = "sea"
                                                       },
                                             Is_Legendary = true
                                         };

            _mockPokeApiClient
                .Setup(i => i.GetPokemonSpecies(It.IsAny<string>()))
                .ReturnsAsync(pokemonSpeciesResponse);

            var sut = new PokemonService(_mockPokeApiClient.Object, _shakespeareTranslator, _yodaTranslator);

            var result = await sut.Get(name);
            result.Name.Should().Be("xxx");
        }
        
        [Test]
        public async Task Get_Should_Throw_PokeApiException_If_Thrown_From_Client()
        {
            var name = "xxx";

            _mockPokeApiClient
                .Setup(i => i.GetPokemonSpecies(It.IsAny<string>()))
                .ThrowsAsync(new PokeApiException("msg"));

            var sut = new PokemonService(_mockPokeApiClient.Object, _shakespeareTranslator, _yodaTranslator);

            _ = sut.Invoking(y => y.Get(name))
                .Should().ThrowAsync<PokeApiException>()
                .WithMessage("msg");
        }

        [Test]
        public async Task GetTranslated_Should_Return_Translated_To_Shakespeare_If_Not_Legendary()
        {
            var name = "xxx";
            var translated = "shakespeare";

            var pokemonSpeciesResponse = new PokemonSpeciesResponse
                                         {
                                             Name = "xxx",
                                             flavor_text_entries = new List<FlavorTextEntry>
                                                                   {
                                                                       new FlavorTextEntry
                                                                       {
                                                                           flavor_text = "original",
                                                                           Language = new Language
                                                                               {
                                                                                   Name = "en"
                                                                               }
                                                                       }
                                                                   },
                                             Habitat = new Habitat
                                                       {
                                                           Name = "sea"
                                                       },
                                             Is_Legendary = false
                                         };

            _mockPokeApiClient
                .Setup(i => i.GetPokemonSpecies(It.IsAny<string>()))
                .ReturnsAsync(pokemonSpeciesResponse);

            _mockFunTranslationsApiClient
                .Setup(i => i.TranslateToShakespeare(It.IsAny<string>()))
                .ReturnsAsync(translated);

            var sut = new PokemonService(_mockPokeApiClient.Object, _shakespeareTranslator, _yodaTranslator);

            var result = await sut.GetTranslated(name);

            result.Should().NotBeNull();
            result.Description.Should().Be(translated);
        }

        [Test]
        public async Task GetTranslated_Should_Return_Translated_To_Yoda_If_Legendary()
        {
            var name = "xxx";
            var translated = "yoda";

            var pokemonSpeciesResponse = new PokemonSpeciesResponse
            {
                Name = "xxx",
                flavor_text_entries = new List<FlavorTextEntry>
                                                                   {
                                                                       new FlavorTextEntry
                                                                       {
                                                                           flavor_text = "original",
                                                                           Language = new Language
                                                                               {
                                                                                   Name = "en"
                                                                               }
                                                                       }
                                                                   },
                Habitat = new Habitat
                {
                    Name = "sea"
                },
                Is_Legendary = true
            };

            _mockPokeApiClient
                .Setup(i => i.GetPokemonSpecies(It.IsAny<string>()))
                .ReturnsAsync(pokemonSpeciesResponse);

            _mockFunTranslationsApiClient
                .Setup(i => i.TranslateToYoda(It.IsAny<string>()))
                .ReturnsAsync(translated);

            var sut = new PokemonService(_mockPokeApiClient.Object, _shakespeareTranslator, _yodaTranslator);

            var result = await sut.GetTranslated(name);

            result.Should().NotBeNull();
            result.Description.Should().Be(translated);
        }

        [Test]
        public async Task GetTranslated_Should_Return_Original_Pokemon_Description_If_Translate_Api_Thrown_An_Exception()
        {
            var name = "xxx";
            var original = "original";

            var pokemonSpeciesResponse = new PokemonSpeciesResponse
                                         {
                                             Name = "xxx",
                                             flavor_text_entries = new List<FlavorTextEntry>
                                                                   {
                                                                       new FlavorTextEntry
                                                                       {
                                                                           flavor_text = original,
                                                                           Language = new Language
                                                                               {
                                                                                   Name = "en"
                                                                               }
                                                                       }
                                                                   },
                                             Habitat = new Habitat
                                                       {
                                                           Name = "sea"
                                                       },
                                             Is_Legendary = true
                                         };

            _mockPokeApiClient
                .Setup(i => i.GetPokemonSpecies(It.IsAny<string>()))
                .ReturnsAsync(pokemonSpeciesResponse);

            _mockFunTranslationsApiClient
                .Setup(i => i.TranslateToYoda(It.IsAny<string>()))
                .ThrowsAsync(new Exception("some timeout"));

            var sut = new PokemonService(_mockPokeApiClient.Object, _shakespeareTranslator, _yodaTranslator);

            var result = await sut.GetTranslated(name);

            result.Should().NotBeNull();
            result.Description.Should().Be(original);
        }
    }
}
