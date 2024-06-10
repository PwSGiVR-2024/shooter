using System;

internal class APIException : Exception
{
    internal APIException() { }

    internal APIException(string message)
        : base(message) { }

    internal APIException(string message, Exception inner)
        : base(message, inner) { }
}