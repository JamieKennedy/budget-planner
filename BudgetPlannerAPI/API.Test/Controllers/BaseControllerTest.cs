using API.Controllers;

using Common.Results.Error.Base;

using FakeItEasy;

using FluentAssertions;

using FluentResults;

using LoggerService.Interfaces;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using Services.Contracts;

namespace API.Test.Controllers
{
    public class BaseControllerTest
    {
        private readonly BaseController _baseController;

        public BaseControllerTest()
        {
            IServiceManager serviceManager = A.Fake<IServiceManager>();
            ILoggerManager loggerManager = A.Fake<ILoggerManager>();
            IHttpContextAccessor httpContextAccessor = A.Fake<IHttpContextAccessor>();

            _baseController = new BaseController(serviceManager, loggerManager, httpContextAccessor);

        }

        [Fact(DisplayName = "MapError returns NotFoundObjectResult with NotFoundError")]
        public void MapError_ReturnsNotFound_WithNotFoundError()
        {
            // Arrange
            var error = new NotFoundError("Item was not found");

            // Act
            var result = _baseController.MapError(error);

            // Assert
            result.Should().BeOfType<NotFoundObjectResult>();
        }

        [Fact(DisplayName = "MapError returns BadRequestObjectResult with BadRequestError")]
        public void MapError_ReturnsBadRequest_WithBadRequestError()
        {
            // Arrange
            var error = new BadRequestError("Bad request");

            // Act
            var result = _baseController.MapError(error);

            // Assert

            result.Should().BeOfType<BadRequestObjectResult>();
        }

        [Fact(DisplayName = "MapError return Unauthorized with UnauthorisedError")]
        public void MapError_ReturnsUnauthorized_WithUnauthorisedError()
        {
            // Arrange
            var error = new UnauthorisedError("Unaouthorised");

            // Act
            var result = _baseController.MapError(error);

            // Assert
            result.Should().BeOfType<UnauthorizedObjectResult>();
        }

        [Fact()]
        public void HandleResult_OneError()
        {
            // Arrange
            var result = Result.Fail("Failed");

            // Act
            var actual = _baseController.HandleResult(result);

            // Assert
            actual.Should().BeOfType<ObjectResult>();

            var obj = (ObjectResult) actual;

            obj.Value.Should().NotBeNull();
            obj.Value.Should().BeOfType<ProblemDetails>();


            var pd = (ProblemDetails) obj.Value!;

            pd.Status.Should().Be(500);
        }
    }
}
