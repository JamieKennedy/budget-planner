using Common.DataTransferObjects.Saving;
using Microsoft.AspNetCore.Mvc;

using Services.Contracts;

namespace API.Controllers
{
    [Route("api/saving")]
    [ApiController]
    public class SavingController : ControllerBase
    {
        private readonly IServiceManager _serviceManager;

        public SavingController(IServiceManager serviceManager)
        {
            _serviceManager = serviceManager;
        }

        [HttpPost(Name = "CreateSaving")]
        public IActionResult Create([FromBody] CreateSavingDto createSavingDto)
        {
            var saving = _serviceManager.SavingService.CreateSaving(createSavingDto);

            return CreatedAtRoute("GetSaving", new { savingId = saving.SavingId }, saving);
        }

        [HttpGet("{savingId}", Name = "GetSaving")]
        public IActionResult Get(long savingId)
        {
            var saving = _serviceManager.SavingService.SelectById(savingId);

            return Ok(saving);
        }
    }
}
