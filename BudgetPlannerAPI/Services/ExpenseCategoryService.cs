using AutoMapper;

using Common.DataTransferObjects.ExpenseCategory;
using Common.Exceptions.ExpenseCategory;
using Common.Exceptions.User;
using Common.Models;

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

        public async Task<ExpenseCategoryDto> CreateExpenseCategory(Guid userId, CreateExpenseCategoryDto createExpenseCategoryDto)
        {
            // Check user exists
            var user = await _userManager.FindByIdAsync(userId.ToString()) ?? throw new UserNotFoundException(userId);

            var expenseCategoryModel = _mapper.Map<ExpenseCategory>(createExpenseCategoryDto);
            expenseCategoryModel.UserId = user.Id;

            var expenseCategory = _repositoryManager.ExpenseCategory.CreateExpenseCategory(expenseCategoryModel);

            return _mapper.Map<ExpenseCategoryDto>(expenseCategory);
        }

        public async void DeleteExpenseCategory(Guid userId, Guid expenseCategoryId)
        {
            var _ = await _userManager.FindByIdAsync(userId.ToString()) ?? throw new UserNotFoundException(userId);

            var expenseCategory = _repositoryManager.ExpenseCategory.SelectById(expenseCategoryId) ?? throw new ExpenseCategoryNotFoundException(expenseCategoryId);

            _repositoryManager.ExpenseCategory.DeleteExpenseCategory(expenseCategory);
        }

        public async Task<ExpenseCategoryDto> SelectById(Guid userId, Guid expenseCategoryId, bool trackChanges = false)
        {
            var _ = await _userManager.FindByIdAsync(userId.ToString()) ?? throw new UserNotFoundException(userId);

            var expenseCategory = _repositoryManager.ExpenseCategory.SelectById(expenseCategoryId, trackChanges) ?? throw new ExpenseCategoryNotFoundException(expenseCategoryId);

            return _mapper.Map<ExpenseCategoryDto>(expenseCategory);
        }

        public async Task<IEnumerable<ExpenseCategoryDto>> SelectByUserId(Guid userId, bool trackChanges = false)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString()) ?? throw new UserNotFoundException(userId);

            var expenseCategories = _repositoryManager.ExpenseCategory.SelectByUserId(user.Id, trackChanges);

            return _mapper.Map<IEnumerable<ExpenseCategoryDto>>(expenseCategories) ?? Enumerable.Empty<ExpenseCategoryDto>();
        }

        public async Task<ExpenseCategoryDto> UpdateExpenseCategory(Guid userId, Guid expenseCategoryId, UpdateExpenseCategoryDto updateExpenseCategoryDto)
        {
            var _ = await _userManager.FindByIdAsync(userId.ToString()) ?? throw new UserNotFoundException(userId);

            var expenseCategory = _repositoryManager.ExpenseCategory.SelectById(expenseCategoryId) ?? throw new ExpenseCategoryNotFoundException(expenseCategoryId);

            expenseCategory.Name = updateExpenseCategoryDto.Name is null ? expenseCategory.Name : updateExpenseCategoryDto.Name;
            expenseCategory.ColourHex = updateExpenseCategoryDto.ColourHex is null ? expenseCategory.ColourHex : updateExpenseCategoryDto.ColourHex;

            var updatedExpenseCategroy = _repositoryManager.ExpenseCategory.UpdateExpenseCategory(expenseCategory);

            return _mapper.Map<ExpenseCategoryDto>(updatedExpenseCategroy);

        }
    }
}
