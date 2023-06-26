using AutoMapper;

using Common.DataTransferObjects.SavingsBalance;
using Common.Exceptions.Savings;
using Common.Exceptions.SavingsBalance;
using Common.Models;

using Microsoft.Extensions.Configuration;

using Repository.Contracts;

using Services.Contracts;

namespace Services
{
    public class SavingsBalanceService : ISavingsBalanceService
    {
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;
        private readonly IRepositoryManager _repositoryManager;

        public SavingsBalanceService(IConfiguration configuration, IMapper mapper, IRepositoryManager repositoryManager)
        {
            _configuration = configuration;
            _mapper = mapper;
            _repositoryManager = repositoryManager;
        }

        public SavingsBalanceDto CreateSavingsBalance(long savingsId, CreateSavingsBalanceDto createSavingsBalanceDto)
        {
            var savings = _repositoryManager.Savings.SelectById(savingsId);

            if (savings is null) throw new SavingsNotFoundException(savingsId);

            var savingsBalanceModel = _mapper.Map<SavingsBalance>(createSavingsBalanceDto);

            var savingsBalance = _repositoryManager.SavingsBalance.CreateSavingsBalance(savingsId, savingsBalanceModel);
            _repositoryManager.Save();

            var savingsBalanceDto = _mapper.Map<SavingsBalanceDto>(savingsBalance);

            return savingsBalanceDto;
        }

        public void DeleteSavingsBalance(long savingsBalanceId)
        {
            var savingsBalance = _repositoryManager.SavingsBalance.SelectById(savingsBalanceId);

            if (savingsBalance is null) throw new SavingsBalanceNotFoundException(savingsBalanceId);

            _repositoryManager.SavingsBalance.DeleteSavingsBalance(savingsBalance);
            _repositoryManager.Save();
        }

        public SavingsBalanceDto SelectById(long savingsBalanceId, bool trackChanges = false)
        {
            var savingsBalance = _repositoryManager.SavingsBalance.SelectById(savingsBalanceId, trackChanges);

            if (savingsBalance is null) throw new SavingsBalanceNotFoundException(savingsBalanceId);

            var savingsBalanceDto = _mapper.Map<SavingsBalanceDto>(savingsBalance);

            return savingsBalanceDto;
        }

        public IEnumerable<SavingsBalanceDto> SelectBySavingsId(long savingsId, bool trackChanges = false)
        {
            var savingsBalancesDto = new List<SavingsBalanceDto>();
            var savingsBalances = _repositoryManager.SavingsBalance.SelectBySavingsId(savingsId, trackChanges);

            savingsBalancesDto.AddRange(_mapper.Map<List<SavingsBalanceDto>>(savingsBalances));

            return savingsBalancesDto;
        }
    }
}
