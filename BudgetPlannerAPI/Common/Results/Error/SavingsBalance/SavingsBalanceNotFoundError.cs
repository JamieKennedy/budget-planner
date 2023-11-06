using Common.Results.Error.Base;

namespace Common.Results.Error.SavingsBalance
{
    public class SavingsBalanceNotFoundError : NotFoundError
    {
        public SavingsBalanceNotFoundError(Guid savingsBalanceId) : base($"No savings balance found with SavingsBalanceId: {savingsBalanceId}")
        {
        }
    }
}
