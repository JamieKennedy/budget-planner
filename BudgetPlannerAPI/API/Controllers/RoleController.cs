using Common.DataTransferObjects.Role;
using Common.Exceptions.Base;

using LoggerService.Interfaces;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using Services.Contracts;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class RoleController : BaseController
    {


        public RoleController(IServiceManager serviceManager, ILoggerManager loggerManager, IHttpContextAccessor contextAccessor) : base(serviceManager, loggerManager, contextAccessor) { }

        [HttpPost(Name = nameof(CreateRole))]
        public async Task<IActionResult> CreateRole([FromBody] CreateRoleDto createRoleDto)
        {
            var result = await serviceManager.RoleService.CreateRole(createRoleDto.RoleName);

            if (result.Succeeded)
            {
                var role = await serviceManager.RoleService.GetByName(createRoleDto.RoleName);

                return CreatedAtAction(nameof(GetRole), new { id = role.Id }, role);
            }

            throw new BadRequestException("An error occured creating the role");
        }

        [HttpGet("{id}", Name = nameof(GetRole))]
        public async Task<IActionResult> GetRole(Guid id)
        {
            var role = await serviceManager.RoleService.GetById(id);

            return Ok(role);
        }
    }
}
