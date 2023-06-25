using Common.Models.SavingBalance.Dto;

using Microsoft.AspNetCore.Mvc;

using Services.Contracts;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SavingBalanceController : ControllerBase
    {
        private readonly IServiceManager _serviceManager;

        public SavingBalanceController(IServiceManager serviceManager)
        {
            _serviceManager = serviceManager;
        }

        [HttpPost(Name = "CreateSavingBalance")]
        public IActionResult CreateSavingBalance([FromBody] CreateSavingBalanceDto createSavingBalanceDto)
        {
            var savingBalance = _serviceManager.SavingBalanceService.CreateSavingBalance(createSavingBalanceDto);

            return CreatedAtRoute("GetSavingBalanceById", new { savingBalance.SavingBalanceId }, savingBalance);
        }

        [HttpGet("id/{savingBalanceId}", Name = "GetSavingBalanceById")]
        public IActionResult GetSavingBalanceById(long savingBalanceId)
        {
            var savingBalance = _serviceManager.SavingBalanceService.SelectById(savingBalanceId);

            return Ok(savingBalance);
        }

        [HttpGet("savingId/{savingId}", Name = "GetSavingBalanceBySavingId")]
        public IActionResult GetSavingBalanceBySavingId(long savingId)
        {
            var savingBalances = _serviceManager.SavingBalanceService.SelectBySavingId(savingId);

            return Ok(savingBalances);
        }
    }
}
