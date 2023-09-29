using Common.DataTransferObjects.Account;

using LoggerService.Interfaces;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using Services.Contracts;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AccountController : BaseController
    {
        public AccountController(IServiceManager serviceManager, ILoggerManager loggerManager, IHttpContextAccessor contextAccessor) : base(serviceManager, loggerManager, contextAccessor) { }

        [HttpGet("{accountId}", Name = nameof(GetAccount))]
        public async Task<IActionResult> GetAccount(Guid userId, Guid accountId)
        {
            var account = await serviceManager.AccountService.SelectById(userId, accountId);

            return Ok(account);
        }

        [HttpGet(Name = nameof(GetAccountsForUser))]
        public async Task<IActionResult> GetAccountsForUser()
        {
            var accounts = await serviceManager.AccountService.SelectByUserId(AuthIdentity.Id);

            return Ok(accounts);
        }

        [HttpPost(Name = nameof(CreateAccount))]
        public async Task<IActionResult> CreateAccount([FromBody] CreateAccountDto createAccountDto)
        {
            var account = await serviceManager.AccountService.CreateAccount(AuthIdentity.Id, createAccountDto);

            return CreatedAtRoute(nameof(GetAccount), new { accountId = account.Id }, account);
        }

        [HttpPatch("{accountId}", Name = nameof(UpdateAccount))]
        public async Task<IActionResult> UpdateAccount(Guid accountId, UpdateAccountDto updateAccountDto)
        {
            var account = await serviceManager.AccountService.UpdateAccount(AuthIdentity.Id, accountId, updateAccountDto);

            return Ok(account);
        }

        [HttpDelete("{accountId}", Name = nameof(DeleteAccount))]
        public async Task<IActionResult> DeleteAccount(Guid accountId)
        {
            await serviceManager.AccountService.DeleteAccount(AuthIdentity.Id, accountId);

            return Accepted();
        }
    }
}
