using Common.DataTransferObjects.Contributor;

using LoggerService.Interfaces;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using Services.Contracts;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ContributorController : BaseController
    {
        public ContributorController(IServiceManager serviceManager, ILoggerManager loggerManager, IHttpContextAccessor contextAccessor) : base(serviceManager, loggerManager, contextAccessor) { }

        [HttpPost(Name = nameof(CreateContributor))]
        public async Task<IActionResult> CreateContributor([FromBody] CreateContributorDto createContributorDto)
        {
            var contributor = await serviceManager.ContributorService.CreateContributor(AuthIdentity.Id, createContributorDto);

            return CreatedAtRoute(nameof(GetContributor), new { contributor.ContributorId }, contributor);
        }

        [HttpDelete("{contributorId}", Name = nameof(DeleteContributor))]
        public IActionResult DeleteContributor(Guid contributorId)
        {
            serviceManager.ContributorService.DeleteContributor(AuthIdentity.Id, contributorId);

            return Ok();
        }

        [HttpPatch("{contributorId}", Name = nameof(UpdateContributor))]
        public async Task<IActionResult> UpdateContributor(Guid contributorId, [FromBody] UpdateContributorDto updateContributorDto)
        {
            var updatedContributor = await serviceManager.ContributorService.UpdateContributor(AuthIdentity.Id, contributorId, updateContributorDto);

            return Ok(updatedContributor);
        }


        [HttpGet("{contributorId}", Name = nameof(GetContributor))]
        public async Task<IActionResult> GetContributor(Guid contributorId)
        {
            var contributor = await serviceManager.ContributorService.SelectById(AuthIdentity.Id, contributorId);

            return Ok(contributor);
        }

        [HttpGet(Name = nameof(GetContributorsByUserId))]
        public async Task<IActionResult> GetContributorsByUserId()
        {
            var contributors = await serviceManager.ContributorService.SelectByUserId(AuthIdentity.Id);

            return Ok(contributors);
        }
    }
}
