using AutoMapper;

using Common.Exceptions.Saving;
using Common.Exceptions.User;
using Common.Models.Saving;
using Common.Models.Saving.Dto;

using Microsoft.Extensions.Configuration;

using Repository.Contracts;

using Services.Contracts;

namespace Services
{
    public class SavingService : ISavingService
    {
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;
        private readonly IRepositoryManager _repositoryManager;

        public SavingService(IConfiguration configuration, IMapper mapper, IRepositoryManager repositoryManager)
        {
            _configuration = configuration;
            _mapper = mapper;
            _repositoryManager = repositoryManager;
        }

        public SavingModel CreateSaving(CreateSavingDto createSavingDto)
        {
            var user = _repositoryManager.User.SelectById(createSavingDto.UserId);

            if (user is null) throw new UserNotFoundException(createSavingDto.UserId);

            var savingModel = _mapper.Map<SavingModel>(createSavingDto);

            var saving = _repositoryManager.Saving.CreateSaving(savingModel);
            _repositoryManager.Save();

            return saving;
        }

        public SavingModel SelectById(long savingId)
        {
            var saving = _repositoryManager.Saving.SelectById(savingId);

            if (saving is null) throw new SavingNotFoundException(savingId);

            return saving;
        }
    }
}
