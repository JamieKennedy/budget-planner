using Common.DataTransferObjects.Base;
using Common.Results.Success;

using FluentResults;

namespace API.Extensions
{
    public static class ResultExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T">Type T of the value of the result where T inherits from DtoBase </typeparam>
        /// <param name="result"></param>
        /// <param name="route">The name of the route in which the resource can be accessed</param>
        /// <returns>The result with an additional success of type Created</returns>
        public static Result<T> WithCreated<T>(this Result<T> result, string route) where T : DtoBase
        {
            if (result.IsSuccess)
            {
                result = result.WithSuccess(new Created<T>(route, new { result.Value.Id }, result.Value));
            }

            return result;
        }

        public static Result WithAccepted(this Result result)
        {
            if (result.IsSuccess)
            {
                result = result.WithSuccess(new Accepted());
            }

            return result;
        }
    }
}
