using Microsoft.AspNetCore.Http;

namespace Common.Exceptions.Base;

public class BadRequestException : Exception, IException
{
    public BadRequestException(string message) : base(message) { }
    public int StatusCode => StatusCodes.Status400BadRequest;
}