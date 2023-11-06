using API.Extensions;

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
            var contributorResult = (await serviceManager.ContributorService.CreateContributor(AuthIdentity.Id, createContributorDto)).WithCreated(nameof(GetContributor));



            return HandleResult(contributorResult);
        }

        [HttpDelete("{contributorId}", Name = nameof(DeleteContributor))]
        public async Task<IActionResult> DeleteContributor(Guid contributorId)
        {
            var result = await serviceManager.ContributorService.DeleteContributor(AuthIdentity.Id, contributorId);

            return HandleResult(result);
        }

        [HttpPatch("{contributorId}", Name = nameof(UpdateContributor))]
        public async Task<IActionResult> UpdateContributor(Guid contributorId, [FromBody] UpdateContributorDto updateContributorDto)
        {
            var updatedContributorResult = await serviceManager.ContributorService.UpdateContributor(AuthIdentity.Id, contributorId, updateContributorDto);

            return HandleResult(updatedContributorResult);
        }


        [HttpGet("{contributorId}", Name = nameof(GetContributor))]
        public async Task<IActionResult> GetContributor(Guid contributorId)
        {
            var contributorResult = await serviceManager.ContributorService.SelectById(AuthIdentity.Id, contributorId);

            return HandleResult(contributorResult);
        }

        [HttpGet(Name = nameof(GetContributorsByUserId))]
        public async Task<IActionResult> GetContributorsByUserId()
        {
            var contributorsResult = await serviceManager.ContributorService.SelectByUserId(AuthIdentity.Id);

            return HandleResult(contributorsResult);
        }
    }
}
