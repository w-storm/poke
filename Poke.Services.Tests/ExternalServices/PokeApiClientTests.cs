using System.Net;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using Poke.Models;
using Poke.Models.Exceptions;
using Poke.Services.ExternalServices;
using Poke.Services.ExternalServices.Responses.PokeApi;
using RestSharp;

namespace Poke.Services.Tests.ExternalServices
{
    [TestFixture]
    public class PokeApiClientTests
    {
        private Mock<IRestClient> _mockRestClient;

        [SetUp]
        public void Setup()
        {
            _mockRestClient = new Mock<IRestClient>();
        }

        [Test]
        public async Task GetPokemonSpecies_Should_Return_Populated_PokemonSpeciesResponse()
        {
            var response = new RestResponse<PokemonSpeciesResponse>
            {
                StatusCode = HttpStatusCode.OK,
                Data = new PokemonSpeciesResponse
                {
                    Name = "test"
                },
                ResponseStatus = ResponseStatus.Completed
            };

            var expectedUrl = "http://mock.com/pokemon-species/test";

            _mockRestClient.Setup(i => i.ExecuteAsync<PokemonSpeciesResponse>(It.IsAny<RestRequest>(),
                                      It.IsAny<CancellationToken>()))
                .ReturnsAsync(response);

            var config = new ConfigBase
                         {
                             ExternalServicesPokeApiUrl = "http://mock.com",
                             ExternalServicesPokeApiTimeoutSeconds = 60
                         };

            var apiClient = new PokeApiClient(_mockRestClient.Object, config);

            var result = await apiClient.GetPokemonSpecies("test");

            _mockRestClient.Verify(i => i.ExecuteAsync<PokemonSpeciesResponse>(It.Is<RestRequest>(m => m.Resource == expectedUrl),
                                       It.IsAny<CancellationToken>()), Times.Once);

            result.Should().NotBeNull();
            result.Name.Should().Be("test");
        }

        [Test]
        public void GetPokemonSpecies_Should_Throw_ApiClientException_If_Request_Not_Successful()
        {
            var response = new RestResponse<PokemonSpeciesResponse>
                           {
                               StatusCode = HttpStatusCode.BadRequest,
                               Data = new PokemonSpeciesResponse
                                      {
                                          Name = "test"
                                      },
                               Content = "bad request"
                           };

            _mockRestClient.Setup(i => i.ExecuteAsync<PokemonSpeciesResponse>(It.IsAny<RestRequest>(),
                                      It.IsAny<CancellationToken>()))
                .ReturnsAsync(response);

            var config = new ConfigBase
                         {
                             ExternalServicesPokeApiUrl = "http://mock.com",
                             ExternalServicesPokeApiTimeoutSeconds = 60
                         };

            var apiClient = new PokeApiClient(_mockRestClient.Object, config);

            _ = apiClient.Invoking(y => apiClient.GetPokemonSpecies("test"))
                .Should().ThrowAsync<PokeApiException>()
                .WithMessage("bad request");
        }
    }
}
