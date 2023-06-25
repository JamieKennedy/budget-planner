using AutoMapper;
using Common.DataTransferObjects.SavingBalance;
using Common.Exceptions.Saving;
using Common.Exceptions.SavingBalance;
using Common.Models;

using Microsoft.Extensions.Configuration;

using Repository.Contracts;

using Services.Contracts;

namespace Services
{
    public class SavingBalanceService : ISavingBalanceService
    {
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;
        private readonly IRepositoryManager _repositoryManager;

        public SavingBalanceService(IConfiguration configuration, IMapper mapper, IRepositoryManager repositoryManager)
        {
            _configuration = configuration;
            _mapper = mapper;
            _repositoryManager = repositoryManager;
        }

        public SavingBalance CreateSavingBalance(CreateSavingBalanceDto createSavingBalanceDto)
        {
            var saving = _repositoryManager.Saving.SelectById(createSavingBalanceDto.SavingId);

            if (saving is null) throw new SavingNotFoundException(createSavingBalanceDto.SavingId);

            var savingBalanceModel = _mapper.Map<SavingBalance>(createSavingBalanceDto);

            var savingBalance = _repositoryManager.SavingBalance.CreateSavingBalance(savingBalanceModel);
            _repositoryManager.Save();

            return savingBalance;
        }

        public void DeleteSavingBalance(long savingBalanceId)
        {
            var savingBalance = _repositoryManager.SavingBalance.SelectById(savingBalanceId);

            if (savingBalance is null) throw new SavingBalanceNotFoundException(savingBalanceId);

            _repositoryManager.SavingBalance.DeleteSavingBalance(savingBalance);
            _repositoryManager.Save();
        }

        public SavingBalance SelectById(long savingBalanceId, bool trackChanges = false)
        {
            var savingBalance = _repositoryManager.SavingBalance.SelectById(savingBalanceId, trackChanges);

            if (savingBalance is null) throw new SavingBalanceNotFoundException(savingBalanceId);

            return savingBalance;
        }

        public IEnumerable<SavingBalance> SelectBySavingId(long savingId, bool trackChanges = false)
        {
            var savingBalances = new List<SavingBalance>();

            savingBalances.AddRange(_repositoryManager.SavingBalance.SelectBySavingId(savingId, trackChanges));

            return savingBalances;
        }
    }
}
