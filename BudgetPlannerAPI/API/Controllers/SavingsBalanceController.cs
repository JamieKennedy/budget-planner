using API.Extensions;

using Common.DataTransferObjects.SavingsBalance;
using Common.Results.Error.SavingsBalance;

using LoggerService.Interfaces;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using Services.Contracts;

namespace API.Controllers
{
    [Route("api/savings/{savingsId}/[controller]")]
    [ApiController]
    [Authorize]
    public class SavingsBalanceController : BaseController
    {

        public SavingsBalanceController(IServiceManager serviceManager, ILoggerManager loggerManager, IHttpContextAccessor contextAccessor) : base(serviceManager, loggerManager, contextAccessor) { }

        [HttpPost(Name = nameof(CreateSavingsBalance))]
        public IActionResult CreateSavingsBalance(Guid savingsId, [FromBody] CreateSavingsBalanceDto createSavingsBalanceDto)
        {
            var savingsBalanceResult = (serviceManager.SavingsBalanceService.CreateSavingsBalance(savingsId, createSavingsBalanceDto)).WithCreated(nameof(GetSavingsBalanceById));

            return HandleResult(savingsBalanceResult);
        }

        [HttpGet("{savingsBalanceId}", Name = nameof(GetSavingsBalanceById))]
        public IActionResult GetSavingsBalanceById(Guid savingsId, Guid savingsBalanceId)
        {
            var savingsBalanceResult = serviceManager.SavingsBalanceService.SelectById(savingsBalanceId);

            if (savingsBalanceResult.IsSuccess)
            {
                if (savingsBalanceResult.Value.SavingsId != savingsId)
                {
                    savingsBalanceResult = savingsBalanceResult.WithError(new InvalidSavingsIdForSavingsBalanceError(savingsBalanceId, savingsId));
                }
            }



            return HandleResult(savingsBalanceResult);
        }

        [HttpGet(Name = nameof(GetSavingsBalanceBySavingsId))]
        public IActionResult GetSavingsBalanceBySavingsId(Guid savingsId)
        {
            var result = serviceManager.SavingsBalanceService.SelectBySavingsId(savingsId);

            return HandleResult(result);
        }

        [HttpDelete("{savingsBalanceId}")]
        public IActionResult DeleteSavingsBalance(Guid savingsBalanceId)
        {
            var result = serviceManager.SavingsBalanceService.DeleteSavingsBalance(savingsBalanceId);

            return HandleResult(result);
        }
    }
}
