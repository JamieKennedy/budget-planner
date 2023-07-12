using Common.DataTransferObjects.SavingsBalance;
using Common.Exceptions.SavingBalance;

using Microsoft.AspNetCore.Mvc;

using Services.Contracts;

namespace API.Controllers
{
    [Route("api/savings/{savingsId}/[controller]")]
    [ApiController]
    public class SavingsBalanceController : ControllerBase
    {
        private readonly IServiceManager _serviceManager;

        public SavingsBalanceController(IServiceManager serviceManager)
        {
            _serviceManager = serviceManager;
        }

        [HttpPost(Name = nameof(CreateSavingsBalance))]
        public IActionResult CreateSavingsBalance(Guid savingsId, [FromBody] CreateSavingsBalanceDto createSavingsBalanceDto)
        {
            var savingsBalance = _serviceManager.SavingsBalanceService.CreateSavingsBalance(savingsId, createSavingsBalanceDto);

            Thread.Sleep(2000);

            return CreatedAtRoute(nameof(GetSavingsBalanceById), new { savingsId, savingsBalance.SavingsBalanceId }, savingsBalance);
        }

        [HttpGet("{savingsBalanceId}", Name = nameof(GetSavingsBalanceById))]
        public IActionResult GetSavingsBalanceById(Guid savingsId, Guid savingsBalanceId)
        {
            var savingsBalance = _serviceManager.SavingsBalanceService.SelectById(savingsBalanceId);

            if (savingsBalance.SavingsId != savingsId) throw new InvalidSavingsIdForSavingsBalance(savingsBalanceId, savingsId);

            return Ok(savingsBalance);
        }

        [HttpGet(Name = nameof(GetSavingsBalanceBySavingsId))]
        public IActionResult GetSavingsBalanceBySavingsId(Guid savingsId)
        {
            var savingsBalances = _serviceManager.SavingsBalanceService.SelectBySavingsId(savingsId);

            return Ok(savingsBalances);
        }
    }
}
