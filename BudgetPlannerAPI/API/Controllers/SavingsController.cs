using Common.DataTransferObjects.Savings;

using Microsoft.AspNetCore.Mvc;

using Services.Contracts;

namespace API.Controllers
{
    [Route("api/savings")]
    [ApiController]
    public class SavingsController : ControllerBase
    {
        private readonly IServiceManager _serviceManager;

        public SavingsController(IServiceManager serviceManager)
        {
            _serviceManager = serviceManager;
        }

        [HttpPost(Name = "CreateSavings")]
        public IActionResult Create([FromBody] CreateSavingsDto createSavingsDto)
        {
            var savings = _serviceManager.SavingsService.CreateSavings(createSavingsDto);

            return CreatedAtRoute("GetSavings", new { savingsId = savings.SavingsId }, savings);
        }

        [HttpGet("{savingsId}", Name = "GetSavings")]
        public IActionResult Get(long savingsId)
        {
            var savings = _serviceManager.SavingsService.SelectById(savingsId);

            return Ok(savings);
        }
    }
}
