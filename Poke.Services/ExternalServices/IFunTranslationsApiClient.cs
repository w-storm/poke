using System.Threading.Tasks;

namespace Poke.Services.ExternalServices
{
    public interface IFunTranslationsApiClient
    {
        Task<string> TranslateToYoda(string text);
        Task<string> TranslateToShakespeare(string text);
    }
}