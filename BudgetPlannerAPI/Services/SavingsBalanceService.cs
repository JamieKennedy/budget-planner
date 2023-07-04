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

        public SavingsBalanceDto CreateSavingsBalance(Guid savingsId, CreateSavingsBalanceDto createSavingsBalanceDto)
        {
            // check savings exists
            _ = _repositoryManager.Savings.SelectById(savingsId) ?? throw new SavingsNotFoundException(savingsId);

            var savingsBalanceModel = _mapper.Map<SavingsBalance>(createSavingsBalanceDto);
            savingsBalanceModel.SavingsId = savingsId;

            var savingsBalance = _repositoryManager.SavingsBalance.CreateSavingsBalance(savingsBalanceModel);
            _repositoryManager.Save();

            var savingsBalanceDto = _mapper.Map<SavingsBalanceDto>(savingsBalance);

            return savingsBalanceDto;
        }

        public void DeleteSavingsBalance(Guid savingsBalanceId)
        {
            var savingsBalance = _repositoryManager.SavingsBalance.SelectById(savingsBalanceId) ?? throw new SavingsBalanceNotFoundException(savingsBalanceId);
            _repositoryManager.SavingsBalance.DeleteSavingsBalance(savingsBalance);
            _repositoryManager.Save();
        }

        public SavingsBalanceDto SelectById(Guid savingsBalanceId, bool trackChanges = false)
        {
            var savingsBalance = _repositoryManager.SavingsBalance.SelectById(savingsBalanceId, trackChanges) ?? throw new SavingsBalanceNotFoundException(savingsBalanceId);
            var savingsBalanceDto = _mapper.Map<SavingsBalanceDto>(savingsBalance);

            return savingsBalanceDto;
        }

        public IEnumerable<SavingsBalanceDto> SelectBySavingsId(Guid savingsId, bool trackChanges = false)
        {
            var savingsBalancesDto = new List<SavingsBalanceDto>();
            var savingsBalances = _repositoryManager.SavingsBalance.SelectBySavingsId(savingsId, trackChanges);

            savingsBalancesDto.AddRange(_mapper.Map<List<SavingsBalanceDto>>(savingsBalances));

            return savingsBalancesDto;
        }
    }
}
