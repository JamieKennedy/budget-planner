using AutoMapper;

using Common.DataTransferObjects.SavingsBalance;
using Common.Models;
using Common.Results.Error.Savings;
using Common.Results.Error.SavingsBalance;

using FluentResults;

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

        public Result<SavingsBalanceDto> CreateSavingsBalance(Guid savingsId, CreateSavingsBalanceDto createSavingsBalanceDto)
        {
            // check savings exists
            var savings = _repositoryManager.Savings.SelectById(savingsId);
            if (savings is null) return new SavingsNotFoundError(savingsId);

            var savingsBalanceModel = _mapper.Map<SavingsBalance>(createSavingsBalanceDto);
            savingsBalanceModel.SavingsId = savingsId;

            var savingsBalance = _repositoryManager.SavingsBalance.CreateSavingsBalance(savingsBalanceModel);
            _repositoryManager.Save();

            var savingsBalanceDto = _mapper.Map<SavingsBalanceDto>(savingsBalance);

            return savingsBalanceDto;
        }

        public Result DeleteSavingsBalance(Guid savingsBalanceId)
        {
            var savingsBalance = _repositoryManager.SavingsBalance.SelectById(savingsBalanceId);
            if (savingsBalance is null) return new SavingsBalanceNotFoundError(savingsBalanceId);

            _repositoryManager.SavingsBalance.DeleteSavingsBalance(savingsBalance);
            _repositoryManager.Save();

            return Result.Ok();
        }

        public Result<SavingsBalanceDto> SelectById(Guid savingsBalanceId, bool trackChanges = false)
        {
            var savingsBalance = _repositoryManager.SavingsBalance.SelectById(savingsBalanceId, trackChanges);
            if (savingsBalance is null) return new SavingsBalanceNotFoundError(savingsBalanceId);

            var savingsBalanceDto = _mapper.Map<SavingsBalanceDto>(savingsBalance);

            return savingsBalanceDto;
        }

        public Result<List<SavingsBalanceDto>> SelectBySavingsId(Guid savingsId, bool trackChanges = false)
        {
            var savingsBalances = _repositoryManager.SavingsBalance.SelectBySavingsId(savingsId, trackChanges);

            var dto = _mapper.Map<List<SavingsBalanceDto>>(savingsBalances) ?? new List<SavingsBalanceDto>();

            return dto;
        }
    }
}
