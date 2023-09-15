using Common.Exceptions.Base;

namespace Common.Exceptions.Account
{
    public class AccountNotFoundException : NotFoundException
    {
        public AccountNotFoundException(Guid accountId) : base($"No account found with account Id: {accountId}") { }
    }
}
