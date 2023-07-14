using Common.DataTransferObjects.ExpenseCategory;

using Microsoft.AspNetCore.Mvc;

using Services.Contracts;

namespace API.Controllers
{
    [Route("api/user/{userId}/[controller]")]
    [ApiController]
    public class ExpenseCategoryController : ControllerBase
    {
        private readonly IServiceManager _serviceManager;

        public ExpenseCategoryController(IServiceManager serviceManager)
        {
            _serviceManager = serviceManager;
        }

        [HttpPost(Name = nameof(CreateExpenseCategory))]
        public async Task<IActionResult> CreateExpenseCategory(Guid userId, [FromBody] CreateExpenseCategoryDto createExpenseCategoryDto)
        {
            var expenseCategory = await _serviceManager.ExpenseCategoryService.CreateExpenseCategory(userId, createExpenseCategoryDto);

            return CreatedAtRoute(nameof(GetExpenseCategory), new { userId, expenseCategory.ExpenseCategoryId }, expenseCategory);
        }

        [HttpPatch("{expenseCategoryId}", Name = nameof(UpdateExpenseCategory))]
        public async Task<IActionResult> UpdateExpenseCategory(Guid userId, Guid expenseCategroyId, [FromBody] UpdateExpenseCategoryDto updateExpenseCategoryDto)
        {
            var updatedExpenseCategory = await _serviceManager.ExpenseCategoryService.UpdateExpenseCategory(userId, expenseCategroyId, updateExpenseCategoryDto);

            return Ok(updatedExpenseCategory);
        }

        [HttpDelete(Name = nameof(DeleteExpenseCategory))]
        public IActionResult DeleteExpenseCategory(Guid userId, Guid expenseCategoryId)
        {
            _serviceManager.ExpenseCategoryService.DeleteExpenseCategory(userId, expenseCategoryId);

            return Ok();
        }

        [HttpGet("{expenseCategoryId}", Name = nameof(GetExpenseCategory))]
        public async Task<IActionResult> GetExpenseCategory(Guid userId, Guid expenseCategoryId)
        {
            var expenseCategory = await _serviceManager.ExpenseCategoryService.SelectById(userId, expenseCategoryId);

            return Ok(expenseCategory);
        }

        [HttpGet(Name = nameof(GetExpenseCategoryByUserId))]
        public async Task<IActionResult> GetExpenseCategoryByUserId(Guid userId)
        {
            var expenseCategories = await _serviceManager.ExpenseCategoryService.SelectByUserId(userId);

            return Ok(expenseCategories);
        }

    }
}
