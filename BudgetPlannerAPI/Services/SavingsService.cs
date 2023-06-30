using AutoMapper;

using Common.DataTransferObjects.Savings;
using Common.Exceptions.Savings;
using Common.Exceptions.User;
using Common.Models;

using Microsoft.Extensions.Configuration;

using Repository.Contracts;

using Services.Contracts;

namespace Services
{
    public class SavingsService : ISavingsService
    {
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;
        private readonly IRepositoryManager _repositoryManager;

        public SavingsService(IConfiguration configuration, IMapper mapper, IRepositoryManager repositoryManager)
        {
            _configuration = configuration;
            _mapper = mapper;
            _repositoryManager = repositoryManager;
        }

        public SavingsDto CreateSavings(long userId, CreateSavingsDto createSavingsDto)
        {
            var user = _repositoryManager.User.SelectById(userId);

            if (user is null) throw new UserNotFoundException(createSavingsDto.UserId);

            var savingsModel = _mapper.Map<Savings>(createSavingsDto);

            var savings = _repositoryManager.Savings.CreateSavings(savingsModel);
            _repositoryManager.Save();

            var savingsDto = _mapper.Map<SavingsDto>(savings);

            return savingsDto;
        }

        public SavingsDto SelectById(long savingsId)
        {
            var savings = _repositoryManager.Savings.SelectById(savingsId);

            if (savings is null) throw new SavingsNotFoundException(savingsId);

            savings.SavingsBalances = _repositoryManager.SavingsBalance.SelectBySavingsId(savings.SavingsId);

            var savingsDto = _mapper.Map<SavingsDto>(savings);

            return savingsDto;
        }
    }
}
