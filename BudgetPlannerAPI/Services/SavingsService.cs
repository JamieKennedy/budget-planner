﻿using AutoMapper;

using Common.DataTransferObjects.Savings;
using Common.Exceptions.Savings;
using Common.Exceptions.User;
using Common.Models;

using Microsoft.AspNetCore.Identity;
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
        private readonly UserManager<User> _userManager;

        public SavingsService(IConfiguration configuration, IMapper mapper, IRepositoryManager repositoryManager, UserManager<User> userManager)
        {
            _configuration = configuration;
            _mapper = mapper;
            _repositoryManager = repositoryManager;
            _userManager = userManager;
        }

        public async Task<SavingsDto> CreateSavings(Guid userId, CreateSavingsDto createSavingsDto)
        {
            // Check user exists
            _ = await _userManager.FindByIdAsync(userId.ToString()) ?? throw new UserNotFoundException(createSavingsDto.UserId);

            var savingsModel = _mapper.Map<Savings>(createSavingsDto);

            var savings = _repositoryManager.Savings.CreateSavings(savingsModel);
            _repositoryManager.Save();

            var savingsDto = _mapper.Map<SavingsDto>(savings);

            return savingsDto;
        }

        public SavingsDto SelectById(Guid savingsId)
        {
            var savings = _repositoryManager.Savings.SelectById(savingsId) ?? throw new SavingsNotFoundException(savingsId);
            savings.SavingsBalances = _repositoryManager.SavingsBalance.SelectBySavingsId(savings.SavingsId);

            var savingsDto = _mapper.Map<SavingsDto>(savings);

            return savingsDto;
        }
    }
}
