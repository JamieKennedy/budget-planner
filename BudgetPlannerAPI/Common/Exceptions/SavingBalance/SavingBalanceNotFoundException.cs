using Common.Exceptions.Base;

namespace Common.Exceptions.SavingBalance
{
    public class SavingBalanceNotFoundException : NotFoundException
    {
        public SavingBalanceNotFoundException(long savingBalanceId) : base($"No saving balance found with SavingBalanceId: {savingBalanceId}")
        {
        }
    }
}
