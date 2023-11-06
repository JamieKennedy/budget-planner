using Common.Results.Error.Base;

namespace Common.Results.Error.Income
{
    public class IncomeNotFoundError : NotFoundError
    {
        public IncomeNotFoundError(Guid incomeId) : base($"No income found with income Id: {incomeId}")
        {
        }
    }
}
