using API.Extensions;

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
        public IncomeController(IServiceManager serviceManager, ILoggerManager loggerManager, IHttpContextAccessor contextAccessor) : base(serviceManager, loggerManager, contextAccessor) { }

        [HttpPost(Name = nameof(PostAccount))]
        public async Task<IActionResult> PostAccount([FromBody] CreateIncomeDto createIncomeDto)
        {
            var incomeResult = (await serviceManager.IncomeService.CreateIncome(AuthIdentity.Id, createIncomeDto)).WithCreated(nameof(GetIncome));

            return HandleResult(incomeResult);
        }

        [HttpGet(Name = nameof(GetIncomeForUser))]
        public async Task<IActionResult> GetIncomeForUser()
        {
            var incomeResult = await serviceManager.IncomeService.SelectByUserId(AuthIdentity.Id);

            return HandleResult(incomeResult);
        }

        [HttpGet("{incomeId}", Name = nameof(GetIncome))]
        public async Task<IActionResult> GetIncome(Guid incomeId)
        {
            var incomeResult = await serviceManager.IncomeService.SelectById(AuthIdentity.Id, incomeId);

            return HandleResult(incomeResult);
        }

        [HttpDelete("{incomeId}", Name = nameof(DeleteIncome))]
        public async Task<IActionResult> DeleteIncome(Guid incomeId)
        {
            var result = (await serviceManager.IncomeService.DeleteIncome(AuthIdentity.Id, incomeId)).WithAccepted();

            return HandleResult(result);
        }

        [HttpPatch("{incomeId}", Name = nameof(UpdateIncome))]
        public async Task<IActionResult> UpdateIncome(Guid incomeId, UpdateIncomeDto updateIncomeDto)
        {
            var updatedIncomeResult = await serviceManager.IncomeService.UpdateIncome(AuthIdentity.Id, incomeId, updateIncomeDto);

            return HandleResult(updatedIncomeResult);
        }
    }
}
