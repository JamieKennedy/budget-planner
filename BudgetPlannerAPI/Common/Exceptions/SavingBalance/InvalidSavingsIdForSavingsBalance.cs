using Common.Exceptions.Base;

namespace Common.Exceptions.SavingBalance
{
    public class InvalidSavingsIdForSavingsBalance : BadRequestException
    {
        public InvalidSavingsIdForSavingsBalance(long savingsBalanceId, long savingsId) : base($"The savings balance with Id: {savingsBalanceId} does not have SavingsId: {savingsId}")
        {
        }
    }
}
