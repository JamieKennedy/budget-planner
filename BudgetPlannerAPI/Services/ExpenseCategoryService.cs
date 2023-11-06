using System.Collections.ObjectModel;

using AutoMapper;

using Common.DataTransferObjects.ExpenseCategory;
using Common.Models;
using Common.Results.Error.ExpenseCategory;
using Common.Results.Error.User;

using FluentResults;

using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;

using Repository.Contracts;

using Services.Contracts;

namespace Services
{
    public class ExpenseCategoryService : IExpenseCategoryService
    {
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;
        private readonly IRepositoryManager _repositoryManager;
        private readonly UserManager<User> _userManager;

        public ExpenseCategoryService(IConfiguration configuration, IMapper mapper, IRepositoryManager repositoryManager, UserManager<User> userManager)
        {
            _configuration = configuration;
            _mapper = mapper;
            _repositoryManager = repositoryManager;
            _userManager = userManager;
        }

        public async Task<Result<ExpenseCategoryDto>> CreateExpenseCategory(Guid userId, CreateExpenseCategoryDto createExpenseCategoryDto)
        {
            // Check user exists
            var user = await _userManager.FindByIdAsync(userId.ToString());
            if (user is null) return Result.Fail(new UserNotFoundError(userId));

            var expenseCategoryModel = _mapper.Map<ExpenseCategory>(createExpenseCategoryDto);
            expenseCategoryModel.UserId = user.Id;

            var expenseCategory = _repositoryManager.ExpenseCategory.CreateExpenseCategory(expenseCategoryModel);

            return _mapper.Map<ExpenseCategoryDto>(expenseCategory);
        }

        public async Task<Result> DeleteExpenseCategory(Guid userId, Guid expenseCategoryId)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());
            if (user is null) return new UserNotFoundError(userId);

            var expenseCategory = _repositoryManager.ExpenseCategory.SelectById(expenseCategoryId);
            if (expenseCategory is null) return new ExpenseCategoryNotFoundError(expenseCategoryId);

            _repositoryManager.ExpenseCategory.DeleteExpenseCategory(expenseCategory);
            _repositoryManager.Save();

            return Result.Ok();
        }

        public async Task<Result<ExpenseCategoryDto>> SelectById(Guid userId, Guid expenseCategoryId, bool trackChanges = false)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());
            if (user is null) return new UserNotFoundError(userId);

            var expenseCategory = _repositoryManager.ExpenseCategory.SelectById(expenseCategoryId, trackChanges);
            if (expenseCategory is null) return new ExpenseCategoryNotFoundError(expenseCategoryId);

            return _mapper.Map<ExpenseCategoryDto>(expenseCategory);
        }

        public async Task<Result<Collection<ExpenseCategoryDto>>> SelectByUserId(Guid userId, bool trackChanges = false)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());
            if (user is null) return new UserNotFoundError(userId);

            var expenseCategories = _repositoryManager.ExpenseCategory.SelectByUserId(user.Id, trackChanges);

            return _mapper.Map<Collection<ExpenseCategoryDto>>(expenseCategories) ?? new Collection<ExpenseCategoryDto>();
        }

        public async Task<Result<ExpenseCategoryDto>> UpdateExpenseCategory(Guid userId, Guid expenseCategoryId, UpdateExpenseCategoryDto updateExpenseCategoryDto)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());
            if (user is null) return new UserNotFoundError(userId);

            var expenseCategory = _repositoryManager.ExpenseCategory.SelectById(expenseCategoryId);
            if (expenseCategory is null) return new ExpenseCategoryNotFoundError(expenseCategoryId);

            expenseCategory.Name = updateExpenseCategoryDto.Name is null ? expenseCategory.Name : updateExpenseCategoryDto.Name;
            expenseCategory.ColourHex = updateExpenseCategoryDto.ColourHex is null ? expenseCategory.ColourHex : updateExpenseCategoryDto.ColourHex;

            var updatedExpenseCategroy = _repositoryManager.ExpenseCategory.UpdateExpenseCategory(expenseCategory);

            return _mapper.Map<ExpenseCategoryDto>(updatedExpenseCategroy);

        }
    }
}
