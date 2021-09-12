namespace Poke.Services.ExternalServices.Requests.FunTranslations
{
    public class TranslateRequest
    {
        public string text { get; }

        public TranslateRequest(string text)
        {
            this.text = text;
        }
    }
}
