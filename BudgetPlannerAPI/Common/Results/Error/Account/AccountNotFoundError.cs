using Common.Results.Error.Base;

namespace Common.Results.Error.Account
{
    public class AccountNotFoundError : NotFoundError
    {
        public AccountNotFoundError(Guid accountId) : base($"No account found with account Id: {accountId}")
        {
        }
    }
}
