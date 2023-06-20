using Microsoft.AspNetCore.Http;

namespace Common.Exceptions.Base;

public class UnauthorisedException : Exception, IException
{
    public UnauthorisedException(string message) : base(message) { }
    public int StatusCode => StatusCodes.Status401Unauthorized;
}