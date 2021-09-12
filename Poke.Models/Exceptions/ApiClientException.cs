using System;

namespace Poke.Models.Exceptions
{
    public class ApiClientException : Exception
    {
        public ApiClientException(string message)
            : base(message) { }

    }
}
