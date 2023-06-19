using Microsoft.AspNetCore.Http;

namespace Common.Exceptions.Base;

public class NotFoundException : Exception, IException {
    public NotFoundException(string message) : base(message) { }
    public int StatusCode => StatusCodes.Status404NotFound;
}