using Common.Models.Saving;

namespace Repository.Contracts
{
    public interface ISavingRepository
    {
        SavingModel CreateSaving(SavingModel createSavingDto);
        SavingModel? SelectById(long savingId, bool trackChanges = false);
    }
}
