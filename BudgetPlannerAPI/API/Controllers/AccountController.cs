using Common.DataTransferObjects.Account;
using Common.Results.Success;

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
            var result = await serviceManager.AccountService.SelectById(userId, accountId);

            return HandleResult(result);
        }

        [HttpGet(Name = nameof(GetAccountsForUser))]
        public async Task<IActionResult> GetAccountsForUser()
        {
            var accounts = await serviceManager.AccountService.SelectByUserId(AuthIdentity.Id);

            return HandleResult(accounts);
        }

        [HttpPost(Name = nameof(CreateAccount))]
        public async Task<IActionResult> CreateAccount([FromBody] CreateAccountDto createAccountDto)
        {
            var accountResult = (await serviceManager.AccountService.CreateAccount(AuthIdentity.Id, createAccountDto));

            if (accountResult.IsSuccess)
            {
                accountResult = accountResult.WithSuccess(new Created<AccountDto>(nameof(GetAccount), new { accountId = accountResult.Value.Id }, accountResult.Value));
            }

            return HandleResult(accountResult);
        }

        [HttpPatch("{accountId}", Name = nameof(UpdateAccount))]
        public async Task<IActionResult> UpdateAccount(Guid accountId, UpdateAccountDto updateAccountDto)
        {
            var result = await serviceManager.AccountService.UpdateAccount(AuthIdentity.Id, accountId, updateAccountDto);

            return HandleResult(result);
        }

        [HttpDelete("{accountId}", Name = nameof(DeleteAccount))]
        public async Task<IActionResult> DeleteAccount(Guid accountId)
        {
            var result = await serviceManager.AccountService.DeleteAccount(AuthIdentity.Id, accountId);

            if (result.IsSuccess)
            {
                result.Successes.Add(new Accepted());
            }

            return HandleResult(result);
        }
    }
}
