using System.Net;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using Poke.Models;
using Poke.Models.Exceptions;
using Poke.Services.ExternalServices;
using Poke.Services.ExternalServices.Responses.FunTranslations;
using RestSharp;

namespace Poke.Services.Tests.ExternalServices
{
    [TestFixture]
    public class FunTranslationsApiClientTests
    {
        private Mock<IRestClient> _mockRestClient;

        [SetUp]
        public void Setup()
        {
            _mockRestClient = new Mock<IRestClient>();
        }

        [Test]
        public async Task TranslateToYoda_Should_Return_Populated_TranslateResponse()
        {
            var response = new RestResponse<TranslateResponse>
                           {
                               StatusCode = HttpStatusCode.OK,
                               Data = new TranslateResponse
                                      {
                                          Contents = new Contents
                                                     {
                                                         Translated = "yoda translated"
                                                     }
                                      },
                               ResponseStatus = ResponseStatus.Completed
            };
        
            var expectedUrl = "http://mock.com/yoda.json";

                               _mockRestClient.Setup(i => i.ExecuteAsync<TranslateResponse>(It.IsAny<RestRequest>(),
                               It.IsAny<CancellationToken>()))
                                   .ReturnsAsync(response);


                               var config = new ConfigBase
                               {
                               ExternalServicesFunTranslationsUrl = "http://mock.com",
                               ExternalServicesFunTranslationsTimeoutSeconds = 60
                           };

            var apiClient = new FunTranslationsApiClient(_mockRestClient.Object, config);

            var result = await apiClient.TranslateToYoda("test");

            _mockRestClient.Verify(i => i.ExecuteAsync<TranslateResponse>(It.Is<RestRequest>(m => m.Resource == expectedUrl),
                                       It.IsAny<CancellationToken>()), Times.Once);

            result.Should().NotBeNull();
            result.Should().Be("yoda translated");
        }

        [Test]
        public async Task TranslateToShakespeare_Should_Return_Populated_TranslateResponse()
        {
            var response = new RestResponse<TranslateResponse>
                           {
                               StatusCode = HttpStatusCode.OK,
                               Data = new TranslateResponse
                                      {
                                          Contents = new Contents
                                                     {
                                                         Translated = "shakespeare translated"
                                          }
                                      },
                               ResponseStatus = ResponseStatus.Completed
                           };

            var expectedUrl = "http://mock.com/shakespeare.json";

            _mockRestClient.Setup(i => i.ExecuteAsync<TranslateResponse>(It.IsAny<RestRequest>(),
                                      It.IsAny<CancellationToken>()))
                .ReturnsAsync(response);


            var config = new ConfigBase
                         {
                             ExternalServicesFunTranslationsUrl = "http://mock.com",
                             ExternalServicesFunTranslationsTimeoutSeconds = 60
                         };

            var apiClient = new FunTranslationsApiClient(_mockRestClient.Object, config);

            var result = await apiClient.TranslateToShakespeare("test");

            _mockRestClient.Verify(i => i.ExecuteAsync<TranslateResponse>(It.Is<RestRequest>(m => m.Resource == expectedUrl),
                                       It.IsAny<CancellationToken>()), Times.Once);

            result.Should().NotBeNull();
            result.Should().Be("shakespeare translated");
        }

        [Test]
        public void TranslateToShakespeare_Should_Throw_ApiClientException_If_Request_Not_Successful()
        {
            var response = new RestResponse<TranslateResponse>
                           {
                               StatusCode = HttpStatusCode.BadRequest,
                               Data = new TranslateResponse
                                      {
                                          Contents = new Contents
                                                     {
                                                         Translated = "shakespeare translated"
                                                     }
                                      }
                           };

            _mockRestClient.Setup(i => i.ExecuteAsync<TranslateResponse>(It.IsAny<RestRequest>(),
                                      It.IsAny<CancellationToken>()))
                .ReturnsAsync(response);

            var config = new ConfigBase
            {
                ExternalServicesPokeApiUrl = "http://mock.com",
                ExternalServicesPokeApiTimeoutSeconds = 60
            };

            var apiClient = new FunTranslationsApiClient(_mockRestClient.Object, config);

            _ = apiClient.Invoking(y => apiClient.TranslateToShakespeare("test"))
                .Should().ThrowAsync<PokeApiException>()
                .WithMessage("bad request");
        }

        [Test]
        public void TranslateToYoda_Should_Throw_ApiClientException_If_Request_Not_Successful()
        {
            var response = new RestResponse<TranslateResponse>
                           {
                               StatusCode = HttpStatusCode.BadRequest,
                               Data = new TranslateResponse
                                      {
                                          Contents = new Contents
                                                     {
                                                         Translated = "yoda translated"
                                                     }
                                      }
                           };

            _mockRestClient.Setup(i => i.ExecuteAsync<TranslateResponse>(It.IsAny<RestRequest>(),
                                      It.IsAny<CancellationToken>()))
                .ReturnsAsync(response);

            var config = new ConfigBase
                         {
                             ExternalServicesPokeApiUrl = "http://mock.com",
                             ExternalServicesPokeApiTimeoutSeconds = 60
                         };

            var apiClient = new FunTranslationsApiClient(_mockRestClient.Object, config);

            _ = apiClient.Invoking(y => apiClient.TranslateToYoda("test"))
                .Should().ThrowAsync<PokeApiException>()
                .WithMessage("bad request");
        }
    }
}
