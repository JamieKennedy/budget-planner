using Common.DataTransferObjects.Savings;

using LoggerService.Interfaces;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using Services.Contracts;

namespace API.Controllers
{
    [Route("api/savings")]
    [ApiController]
    [Authorize]
    public class SavingsController : BaseController
    {
        public SavingsController(IServiceManager serviceManager, ILoggerManager loggerManager, IHttpContextAccessor contextAccessor) : base(serviceManager, loggerManager, contextAccessor) { }


        [HttpPost(Name = nameof(CreateSavings))]
        public async Task<IActionResult> CreateSavings([FromBody] CreateSavingsDto createSavingsDto)
        {
            var savings = await serviceManager.SavingsService.CreateSavings(AuthIdentity.Id, createSavingsDto);

            return CreatedAtRoute(nameof(GetSavings), new { savings.SavingsId }, savings);
        }

        [HttpGet("{savingsId}", Name = nameof(GetSavings))]
        public IActionResult GetSavings(Guid savingsId)
        {
            var savings = serviceManager.SavingsService.SelectById(savingsId);

            return Ok(savings);
        }

        [HttpGet(Name = nameof(GetSavingsForUser))]
        public async Task<IActionResult> GetSavingsForUser()
        {
            var savings = await serviceManager.SavingsService.SelectByUserId(AuthIdentity.Id);

            return Ok(savings);
        }

        [HttpDelete("{savingsId}", Name = nameof(DeleteSavings))]
        public async Task<IActionResult> DeleteSavings(Guid SavingsId)
        {
            await serviceManager.SavingsService.DeleteById(AuthIdentity.Id, SavingsId);

            return Ok();
        }

        [HttpPatch("{savingsId}", Name = nameof(PatchSavings))]
        public async Task<IActionResult> PatchSavings(Guid savingsId, [FromBody] UpdateSavingsDto updateSavingsDto)
        {
            var updatedSavings = await serviceManager.SavingsService.UpdateSavings(AuthIdentity.Id, savingsId, updateSavingsDto);

            return Ok(updatedSavings);
        }
    }
}
