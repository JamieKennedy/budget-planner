using Common.DataTransferObjects.Saving;
using Common.Models;

namespace Services.Contracts
{
    public interface ISavingService
    {
        Saving CreateSaving(CreateSavingDto createSavingDto);
        Saving SelectById(long savingId);
    }
}
