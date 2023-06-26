using Common.DataTransferObjects.SavingsBalance;

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

        [HttpPost]
        public IActionResult CreateSavingsBalance(long savingsId, [FromBody] CreateSavingsBalanceDto createSavingsBalanceDto)
        {
            var savingsBalance = _serviceManager.SavingsBalanceService.CreateSavingsBalance(savingsId, createSavingsBalanceDto);

            return CreatedAtRoute("GetSavingsBalanceById", new { savingsId, savingsBalance.SavingsBalanceId }, savingsBalance);
        }

        [HttpGet("id/{savingsBalanceId}", Name = "GetSavingsBalanceById")]
        public IActionResult GetSavingsBalanceById(long savingsBalanceId)
        {
            var savingsBalance = _serviceManager.SavingsBalanceService.SelectById(savingsBalanceId);

            return Ok(savingsBalance);
        }

        [HttpGet(Name = "GetSavingsBalanceBySavingsId")]
        public IActionResult GetSavingsBalanceBySavingsId(long savingsId)
        {
            var savingsBalances = _serviceManager.SavingsBalanceService.SelectBySavingsId(savingsId);

            return Ok(savingsBalances);
        }
    }
}
