using Common.Exceptions.Base;

namespace Common.Exceptions.Account
{
    public class IncomeNotFoundException : NotFoundException
    {
        public IncomeNotFoundException(Guid incomeId) : base($"No income found with income Id: {incomeId}") { }
    }
}
