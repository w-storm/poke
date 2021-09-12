using System;
using System.Threading.Tasks;
using Poke.Models;
using Poke.Models.Exceptions;
using Poke.Services.ExternalServices.Requests.FunTranslations;
using Poke.Services.ExternalServices.Responses.FunTranslations;
using RestSharp;

namespace Poke.Services.ExternalServices
{
    public class FunTranslationsApiClient : ApiClientBase, IFunTranslationsApiClient
    {
        private readonly string _baseUrl;
        private readonly int _timeoutInSeconds;

        public FunTranslationsApiClient(IRestClient restClient, IConfig config)
            : base(restClient)
        {
            _baseUrl = config.ExternalServicesFunTranslationsUrl;
            _timeoutInSeconds = config.ExternalServicesFunTranslationsTimeoutSeconds;
        }

        public async Task<string> TranslateToYoda(string text)
        {
            var url = $"{_baseUrl}/yoda.json";

            var response = await Translate(url, text);

            return response.Contents.Translated;
        }

        public async Task<string> TranslateToShakespeare(string text)
        {
            var url = $"{_baseUrl}/shakespeare.json";

            var response = await Translate(url, text);

            return response.Contents.Translated;
        }

        private async Task<TranslateResponse> Translate(string url, string text)
        {
            var request = new TranslateRequest(text);

            try
            {
                return await Post<TranslateResponse>(url, _timeoutInSeconds, request);
            }
            catch (Exception ex)
            {
                throw new FunTranslationsException(ex.Message);
            }
        }


    }
}
