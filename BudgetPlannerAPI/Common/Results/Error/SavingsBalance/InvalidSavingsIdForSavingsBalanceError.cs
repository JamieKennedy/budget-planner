using Common.Results.Error.Base;

namespace Common.Results.Error.SavingsBalance
{
    public class InvalidSavingsIdForSavingsBalanceError : BadRequestError
    {
        public InvalidSavingsIdForSavingsBalanceError(Guid savingsBalanceId, Guid savingsId) : base($"The savings balance with Id: {savingsBalanceId} does not have SavingsId: {savingsId}") { }
    }
}
