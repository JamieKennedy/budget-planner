using Common.DataTransferObjects.Account;

using Microsoft.AspNetCore.Mvc;

using Services.Contracts;

namespace API.Controllers
{
    [Route("api/user/{userId}/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        public IServiceManager _serviceManager;

        public AccountController(IServiceManager serviceManager)
        {
            _serviceManager = serviceManager;
        }

        [HttpGet("{accountId}", Name = nameof(GetAccount))]
        public async Task<IActionResult> GetAccount(Guid userId, Guid accountId)
        {
            var account = await _serviceManager.AccountService.SelectById(userId, accountId);

            return Ok(account);
        }

        [HttpGet(Name = nameof(GetAccountsForUser))]
        public async Task<IActionResult> GetAccountsForUser(Guid userId)
        {
            var accounts = await _serviceManager.AccountService.SelectByUserId(userId);

            return Ok(accounts);
        }

        [HttpPost(Name = nameof(CreateAccount))]
        public async Task<IActionResult> CreateAccount(Guid userId, [FromBody] CreateAccountDto createAccountDto)
        {
            var account = await _serviceManager.AccountService.CreateAccount(userId, createAccountDto);

            return CreatedAtRoute(nameof(GetAccount), new { userId, accountId = account.Id }, account);
        }

        [HttpPatch("{accountId}", Name = nameof(UpdateAccount))]
        public async Task<IActionResult> UpdateAccount(Guid userId, Guid accountId, UpdateAccountDto updateAccountDto)
        {
            var account = await _serviceManager.AccountService.UpdateAccount(userId, accountId, updateAccountDto);

            return Ok(account);
        }

        [HttpDelete("{accountId}", Name = nameof(DeleteAccount))]
        public async Task<IActionResult> DeleteAccount(Guid userId, Guid accountId)
        {
            await _serviceManager.AccountService.DeleteAccount(userId, accountId);

            return Accepted();
        }
    }
}
