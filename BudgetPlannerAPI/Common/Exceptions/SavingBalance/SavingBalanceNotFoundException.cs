using Common.Exceptions.Base;

namespace Common.Exceptions.SavingsBalance
{
    public class SavingsBalanceNotFoundException : NotFoundException
    {
        public SavingsBalanceNotFoundException(Guid savingsBalanceId) : base($"No savings balance found with SavingsBalanceId: {savingsBalanceId}")
        {
        }
    }
}
