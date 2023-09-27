using Common.DataTransferObjects.Income;

using LoggerService.Interfaces;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using Services.Contracts;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class IncomeController : BaseController
    {
        public IncomeController(IServiceManager serviceManager, ILoggerManager loggerManager) : base(serviceManager, loggerManager) { }

        [HttpPost(Name = nameof(PostAccount))]
        public async Task<IActionResult> PostAccount([FromBody] CreateIncomeDto createIncomeDto)
        {
            var income = await serviceManager.IncomeService.CreateIncome(authIdentity.Id, createIncomeDto);

            return CreatedAtRoute(nameof(GetIncome), new { incomeId = income.Id }, income);
        }

        [HttpGet(Name = nameof(GetIncomeForUser))]
        public async Task<IActionResult> GetIncomeForUser()
        {
            var income = await serviceManager.IncomeService.SelectByUserId(authIdentity.Id);

            return Ok(income);
        }

        [HttpGet("{incomeId}", Name = nameof(GetIncome))]
        public async Task<IActionResult> GetIncome(Guid incomeId)
        {
            var income = await serviceManager.IncomeService.SelectById(authIdentity.Id, incomeId);

            return Ok(income);
        }

        [HttpDelete("{incomeId}", Name = nameof(DeleteIncome))]
        public async Task<IActionResult> DeleteIncome(Guid incomeId)
        {
            await serviceManager.IncomeService.DeleteIncome(authIdentity.Id, incomeId);

            return Accepted();
        }

        [HttpPatch("{incomeId}", Name = nameof(UpdateIncome))]
        public async Task<IActionResult> UpdateIncome(Guid incomeId, UpdateIncomeDto updateIncomeDto)
        {
            var updatedIncome = await serviceManager.IncomeService.UpdateIncome(authIdentity.Id, incomeId, updateIncomeDto);

            return Ok(updatedIncome);
        }
    }
}
