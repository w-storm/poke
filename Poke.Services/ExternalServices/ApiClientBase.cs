using RestSharp;
using System;
using System.Threading;
using System.Threading.Tasks;
using Poke.Models.Exceptions;

namespace Poke.Services.ExternalServices
{
    public class ApiClientBase
    {
        private readonly IRestClient _restClient;

        public ApiClientBase(IRestClient restClient)
        {
            _restClient = restClient;
        }

        protected async Task<T> Post<T>(string url, int timeoutInSeconds, object payload)
        {
            var request = new RestRequest(url, Method.POST)
            {
                Timeout = timeoutInSeconds * 1000,
                RequestFormat = DataFormat.Json
            };

            request.AddJsonBody(payload);

            return await Execute<T>(request);
        }

        protected async Task<T> Get<T>(string url, int timeoutInSeconds)
        {
            var request = new RestRequest(url, Method.GET)
              {
                  Timeout = timeoutInSeconds * 1000
              };
            
            return await Execute<T>(request);
        }

        private async Task<T> Execute<T>(RestRequest request)
        {
            var cancellationTokenSource = new CancellationTokenSource();

            try
            {
                var response = await _restClient.ExecuteAsync<T>(request, cancellationTokenSource.Token);

                // to return more meaningful message, such as 'not found' and avoid parsing issues
                if (!response.IsSuccessful)
                {
                    throw new ApiClientException(((RestResponseBase)response).Content);
                }

                return response.Data;
            }
            catch (Exception exception)
            {
                throw new ApiClientException(exception.Message);
            }
        }
    }
}
