using Common.Models;

namespace Repository.Contracts
{
    public interface ISavingRepository
    {
        Saving CreateSaving(Saving createSavingDto);
        Saving? SelectById(long savingId, bool trackChanges = false);
    }
}
