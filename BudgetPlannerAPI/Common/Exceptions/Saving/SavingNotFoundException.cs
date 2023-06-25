using Common.Exceptions.Base;

namespace Common.Exceptions.Saving
{
    public class SavingNotFoundException : NotFoundException
    {
        public SavingNotFoundException(long savingId) : base($"No saving found with SavingId: {savingId}")
        {
        }
    }
}
