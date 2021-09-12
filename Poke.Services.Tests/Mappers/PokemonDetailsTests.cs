using System.Collections.Generic;
using FluentAssertions;
using NUnit.Framework;
using Poke.Services.ExternalServices.Responses;
using Poke.Services.ExternalServices.Responses.PokeApi;
using Poke.Services.Mappers;

namespace Poke.Services.Tests.Mappers
{
    [TestFixture]
    public class PokemonDetailsTests
    {
        [Test]
        public void FromPokemonSpeciesResponse_Should_Return_Null_If_Input_Value_Is_Null()
        {
            var result = PokemonDetail.FromPokemonSpeciesResponse(null);

            result.Should().BeNull();
        }

        [Test]
        public void FromPokemonSpeciesResponse_Should_Return_Populated_PokemonDetails()
        {
            var pokemonSpeciesResponse = new PokemonSpeciesResponse
                                         {
                Name = "test",
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

            var result = pokemonSpeciesResponse.FromPokemonSpeciesResponse();

            result.Name.Should().Be(pokemonSpeciesResponse.Name);
            result.Description.Should().Be("xxx");
            result.IsLegendary.Should().BeTrue();
            result.Habitat.Should().Be("sea");
        }

        [Test]
        public void FromPokemonSpeciesResponse_Should_Return_Populated_PokemonDetails_But_Habitat_Null_If_Input_Habitat_Value_Is_Null()
        {
            var pokemonSpeciesResponse = new PokemonSpeciesResponse
                                         {
                                             Name = "test",
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
                                             Is_Legendary = true
                                         };

            var result = pokemonSpeciesResponse.FromPokemonSpeciesResponse();

            result.Habitat.Should().BeNull();
        }

        [Test]
        public void FromPokemonSpeciesResponse_Should_Return_Populated_PokemonDetails_But_Description_Null_If_Input_flavor_text_entries_Value_Is_Null()
        {
            var pokemonSpeciesResponse = new PokemonSpeciesResponse
                                         {
                                             Name = "test",
                                             Habitat = new Habitat
                                                       {
                                                           Name = "sea"
                                                       },
                                             Is_Legendary = true
                                         };

            var result = pokemonSpeciesResponse.FromPokemonSpeciesResponse();

            result.Description.Should().BeNull();
        }

        [Test]
        public void FromPokemonSpeciesResponse_Should_Return_Populated_PokemonDetails_With_First_English_Description()
        {
            var pokemonSpeciesResponse = new PokemonSpeciesResponse
                                         {
                                             flavor_text_entries = new List<FlavorTextEntry>
                                                                   {
                                                                       new FlavorTextEntry
                                                                       {
                                                                           flavor_text = "aaa",
                                                                           Language = new Language
                                                                               {
                                                                                   Name = "en"
                                                                               }
                                                                       },
                                                                       new FlavorTextEntry
                                                                       {
                                                                           flavor_text = "bbb",
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

            var result = pokemonSpeciesResponse.FromPokemonSpeciesResponse();

            result.Description.Should().Be("aaa");
        }
    }
}
