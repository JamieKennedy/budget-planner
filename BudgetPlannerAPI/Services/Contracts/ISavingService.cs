using Common.Models.Saving;
using Common.Models.Saving.Dto;

namespace Services.Contracts
{
    public interface ISavingService
    {
        SavingModel CreateSaving(CreateSavingDto createSavingDto);
        SavingModel SelectById(long savingId);
    }
}
