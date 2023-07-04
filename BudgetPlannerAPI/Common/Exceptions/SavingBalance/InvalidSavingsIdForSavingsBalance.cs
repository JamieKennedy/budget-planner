using Common.Exceptions.Base;

namespace Common.Exceptions.SavingBalance
{
    public class InvalidSavingsIdForSavingsBalance : BadRequestException
    {
        public InvalidSavingsIdForSavingsBalance(Guid savingsBalanceId, Guid savingsId) : base($"The savings balance with Id: {savingsBalanceId} does not have SavingsId: {savingsId}")
        {
        }
    }
}
