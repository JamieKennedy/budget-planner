using Common.DataTransferObjects.Contributor;

using Microsoft.AspNetCore.Mvc;

using Services.Contracts;

namespace API.Controllers
{
    [Route("api/user/{userId}/[controller]")]
    [ApiController]
    public class ContributorController : ControllerBase
    {
        private readonly IServiceManager _serviceManager;

        public ContributorController(IServiceManager serviceManager)
        {
            _serviceManager = serviceManager;
        }

        [HttpPost(Name = nameof(CreateContributor))]
        public async Task<IActionResult> CreateContributor(Guid userId, [FromBody] CreateContributorDto createContributorDto)
        {
            var contributor = await _serviceManager.ContributorService.CreateContributor(userId, createContributorDto);

            return CreatedAtRoute(nameof(GetContributor), new { userId, contributor.ContributorId }, contributor);
        }

        [HttpDelete("{contributorId}", Name = nameof(DeleteContributor))]
        public IActionResult DeleteContributor(Guid userId, Guid contributorId)
        {
            _serviceManager.ContributorService.DeleteContributor(userId, contributorId);

            return Ok();
        }

        [HttpPatch("{contributorId}", Name = nameof(UpdateContributor))]
        public async Task<IActionResult> UpdateContributor(Guid userId, Guid contributorId, [FromBody] UpdateContributorDto updateContributorDto)
        {
            var updatedContributor = await _serviceManager.ContributorService.UpdateContributor(userId, contributorId, updateContributorDto);

            return Ok(updatedContributor);
        }


        [HttpGet("{contributorId}", Name = nameof(GetContributor))]
        public async Task<IActionResult> GetContributor(Guid userId, Guid contributorId)
        {
            var contributor = await _serviceManager.ContributorService.SelectById(userId, contributorId);

            return Ok(contributor);
        }

        [HttpGet(Name = nameof(GetContributorsByUserId))]
        public async Task<IActionResult> GetContributorsByUserId(Guid userId)
        {
            var contributors = await _serviceManager.ContributorService.SelectByUserId(userId);

            return Ok(contributors);
        }
    }
}
