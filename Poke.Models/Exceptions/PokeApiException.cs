using System;

namespace Poke.Models.Exceptions
{
    public class PokeApiException : Exception
    {
        public PokeApiException(string message)
            : base(message) { }

    }
}
