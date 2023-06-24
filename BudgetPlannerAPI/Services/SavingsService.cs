using AutoMapper;

using Common.Exceptions.Savings;
using Common.Exceptions.User;
using Common.Models.Savings;
using Common.Models.Savings.Dto;

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

        public SavingsModel CreateSavings(CreateSavingsDto createSavingsDto)
        {
            var user = _repositoryManager.User.SelectById(createSavingsDto.UserId);

            if (user is null) throw new UserNotFoundException(createSavingsDto.UserId);

            var savingsModel = _mapper.Map<SavingsModel>(createSavingsDto);

            var savings = _repositoryManager.Savings.CreateSavings(savingsModel);
            _repositoryManager.Save();

            return savings;
        }

        public SavingsModel SelectById(long savingsId)
        {
            var savings = _repositoryManager.Savings.SelectById(savingsId);

            if (savings is null) throw new SavingsNotFoundException(savingsId);

            return savings;
        }
    }
}
