using Common.Results.Error.Base;

namespace Common.Results.Error.Savings
{
    public class SavingsNotFoundError : NotFoundError
    {
        public SavingsNotFoundError(Guid savingsId) : base($"No savings found with SavingsId: {savingsId}")
        {
        }
    }
}
