using Common.DataTransferObjects.Savings;

using Microsoft.AspNetCore.Mvc;

using Services.Contracts;

namespace API.Controllers
{
    [Route("api/user/{userId}/savings")]
    [ApiController]
    public class SavingsController : ControllerBase
    {
        private readonly IServiceManager _serviceManager;

        public SavingsController(IServiceManager serviceManager)
        {
            _serviceManager = serviceManager;
        }

        [HttpPost(Name = nameof(CreateSavings))]
        public async Task<IActionResult> CreateSavings(Guid userId, [FromBody] CreateSavingsDto createSavingsDto)
        {
            var savings = await _serviceManager.SavingsService.CreateSavings(userId, createSavingsDto);

            return CreatedAtRoute(nameof(GetSavings), new { savingsId = savings.SavingsId }, savings);
        }

        [HttpGet("{savingsId}", Name = nameof(GetSavings))]
        public IActionResult GetSavings(Guid savingsId)
        {
            var savings = _serviceManager.SavingsService.SelectById(savingsId);

            return Ok(savings);
        }
    }
}
