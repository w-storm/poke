using System;
using System.Threading.Tasks;
using Poke.Models;
using Poke.Models.Exceptions;
using Poke.Services.ExternalServices.Responses.PokeApi;
using RestSharp;

namespace Poke.Services.ExternalServices
{
    public class PokeApiClient : ApiClientBase, IPokeApiClient
    {
        private readonly string _baseUrl;
        private readonly int _timeoutInSeconds;

        public PokeApiClient(IRestClient restClient, IConfig config)
            : base(restClient)
        {
            _baseUrl = config.ExternalServicesPokeApiUrl;
            _timeoutInSeconds = config.ExternalServicesPokeApiTimeoutSeconds;
        }

        public async Task<PokemonSpeciesResponse> GetPokemonSpecies(string name)
        {
            
            var url = $"{_baseUrl}/pokemon-species/{name}";

            try
            {
                return await Get<PokemonSpeciesResponse>(url, _timeoutInSeconds);
            }
            catch (Exception ex)
            {
                throw new PokeApiException(ex.Message);
            }
        }
    }
}
