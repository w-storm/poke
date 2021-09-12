using System;

namespace Poke.Models.Exceptions
{
    public class FunTranslationsException : Exception
    {
        public FunTranslationsException(string message)
            : base(message) { }

    }
}
