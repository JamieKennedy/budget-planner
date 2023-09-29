using Common.DataTransferObjects.SavingsBalance;
using Common.Exceptions.SavingBalance;

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
            var savingsBalance = serviceManager.SavingsBalanceService.CreateSavingsBalance(savingsId, createSavingsBalanceDto);

            return CreatedAtRoute(nameof(GetSavingsBalanceById), new { savingsId, savingsBalance.SavingsBalanceId }, savingsBalance);
        }

        [HttpGet("{savingsBalanceId}", Name = nameof(GetSavingsBalanceById))]
        public IActionResult GetSavingsBalanceById(Guid savingsId, Guid savingsBalanceId)
        {
            var savingsBalance = serviceManager.SavingsBalanceService.SelectById(savingsBalanceId);

            if (savingsBalance.SavingsId != savingsId) throw new InvalidSavingsIdForSavingsBalance(savingsBalanceId, savingsId);

            return Ok(savingsBalance);
        }

        [HttpGet(Name = nameof(GetSavingsBalanceBySavingsId))]
        public IActionResult GetSavingsBalanceBySavingsId(Guid savingsId)
        {
            var savingsBalances = serviceManager.SavingsBalanceService.SelectBySavingsId(savingsId);

            return Ok(savingsBalances);
        }

        [HttpDelete("{savingsBalanceId}")]
        public IActionResult DeleteSavingsBalance(Guid savingsBalanceId)
        {
            serviceManager.SavingsBalanceService.DeleteSavingsBalance(savingsBalanceId);

            return Ok();
        }
    }
}
