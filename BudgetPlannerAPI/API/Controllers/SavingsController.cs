using API.Extensions;

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
            var result = (await serviceManager.SavingsService.CreateSavings(AuthIdentity.Id, createSavingsDto)).WithCreated(nameof(GetSavings));

            return HandleResult(result);
        }

        [HttpGet("{savingsId}", Name = nameof(GetSavings))]
        public IActionResult GetSavings(Guid savingsId)
        {
            var result = serviceManager.SavingsService.SelectById(savingsId);

            return HandleResult(result);
        }

        [HttpGet(Name = nameof(GetSavingsForUser))]
        public async Task<IActionResult> GetSavingsForUser()
        {
            var result = await serviceManager.SavingsService.SelectByUserId(AuthIdentity.Id);

            return HandleResult(result);
        }

        [HttpDelete("{savingsId}", Name = nameof(DeleteSavings))]
        public async Task<IActionResult> DeleteSavings(Guid SavingsId)
        {
            var result = await serviceManager.SavingsService.DeleteById(AuthIdentity.Id, SavingsId);

            return HandleResult(result);
        }

        [HttpPatch("{savingsId}", Name = nameof(PatchSavings))]
        public async Task<IActionResult> PatchSavings(Guid savingsId, [FromBody] UpdateSavingsDto updateSavingsDto)
        {
            var result = await serviceManager.SavingsService.UpdateSavings(AuthIdentity.Id, savingsId, updateSavingsDto);

            return HandleResult(result);
        }
    }
}
