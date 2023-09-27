using Common.DataTransferObjects.Authentication;
using Common.DataTransferObjects.Income;
using Common.Utils;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using Services.Contracts;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class IncomeController : ControllerBase
    {
        public IServiceManager _serviceManager;

        public IncomeController(IServiceManager serviceManager)
        {
            _serviceManager = serviceManager;
        }

        [HttpPost(Name = nameof(PostAccount))]
        public async Task<IActionResult> PostAccount([FromBody] CreateIncomeDto createIncomeDto)
        {
            AuthIdentity identity = HttpContext.GetAuthIdentity();

            var income = await _serviceManager.IncomeService.CreateIncome(identity.Id, createIncomeDto);

            return CreatedAtRoute(nameof(GetIncome), new { incomeId = income.Id }, income);
        }

        [HttpGet(Name = nameof(GetIncomeForUser))]
        public async Task<IActionResult> GetIncomeForUser()
        {
            AuthIdentity identity = HttpContext.GetAuthIdentity();

            var income = await _serviceManager.IncomeService.SelectByUserId(identity.Id);

            return Ok(income);
        }

        [HttpGet("{incomeId}", Name = nameof(GetIncome))]
        public async Task<IActionResult> GetIncome(Guid incomeId)
        {
            AuthIdentity identity = HttpContext.GetAuthIdentity();

            var income = await _serviceManager.IncomeService.SelectById(identity.Id, incomeId);

            return Ok(income);
        }

        [HttpDelete("{incomeId}", Name = nameof(DeleteIncome))]
        public async Task<IActionResult> DeleteIncome(Guid incomeId)
        {
            AuthIdentity identity = HttpContext.GetAuthIdentity();

            await _serviceManager.IncomeService.DeleteIncome(identity.Id, incomeId);

            return Accepted();
        }

        [HttpPatch("{incomeId}", Name = nameof(UpdateIncome))]
        public async Task<IActionResult> UpdateIncome(Guid incomeId, UpdateIncomeDto updateIncomeDto)
        {
            AuthIdentity identity = HttpContext.GetAuthIdentity();

            var updatedIncome = await _serviceManager.IncomeService.UpdateIncome(identity.Id, incomeId, updateIncomeDto);

            return Ok(updatedIncome);
        }
    }
}
