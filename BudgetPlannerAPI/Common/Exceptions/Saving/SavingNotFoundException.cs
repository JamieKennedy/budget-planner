using Common.Exceptions.Base;

namespace Common.Exceptions.Savings
{
    public class SavingsNotFoundException : NotFoundException
    {
        public SavingsNotFoundException(Guid savingsId) : base($"No savings found with SavingsId: {savingsId}")
        {
        }
    }
}
