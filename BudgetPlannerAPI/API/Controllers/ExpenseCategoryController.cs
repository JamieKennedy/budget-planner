using Common.DataTransferObjects.ExpenseCategory;

using LoggerService.Interfaces;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using Services.Contracts;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ExpenseCategoryController : BaseController
    {
        public ExpenseCategoryController(IServiceManager serviceManager, ILoggerManager loggerManager, IHttpContextAccessor contextAccessor) : base(serviceManager, loggerManager, contextAccessor) { }

        [HttpPost(Name = nameof(CreateExpenseCategory))]
        public async Task<IActionResult> CreateExpenseCategory([FromBody] CreateExpenseCategoryDto createExpenseCategoryDto)
        {
            var expenseCategory = await serviceManager.ExpenseCategoryService.CreateExpenseCategory(AuthIdentity.Id, createExpenseCategoryDto);

            return CreatedAtRoute(nameof(GetExpenseCategory), new { expenseCategory.ExpenseCategoryId }, expenseCategory);
        }

        [HttpPatch("{expenseCategoryId}", Name = nameof(UpdateExpenseCategory))]
        public async Task<IActionResult> UpdateExpenseCategory(Guid expenseCategroyId, [FromBody] UpdateExpenseCategoryDto updateExpenseCategoryDto)
        {
            var updatedExpenseCategory = await serviceManager.ExpenseCategoryService.UpdateExpenseCategory(AuthIdentity.Id, expenseCategroyId, updateExpenseCategoryDto);

            return Ok(updatedExpenseCategory);
        }

        [HttpDelete(Name = nameof(DeleteExpenseCategory))]
        public IActionResult DeleteExpenseCategory(Guid expenseCategoryId)
        {
            serviceManager.ExpenseCategoryService.DeleteExpenseCategory(AuthIdentity.Id, expenseCategoryId);

            return Ok();
        }

        [HttpGet("{expenseCategoryId}", Name = nameof(GetExpenseCategory))]
        public async Task<IActionResult> GetExpenseCategory(Guid expenseCategoryId)
        {
            var expenseCategory = await serviceManager.ExpenseCategoryService.SelectById(AuthIdentity.Id, expenseCategoryId);

            return Ok(expenseCategory);
        }

        [HttpGet(Name = nameof(GetExpenseCategoryByUserId))]
        public async Task<IActionResult> GetExpenseCategoryByUserId()
        {
            var expenseCategories = await serviceManager.ExpenseCategoryService.SelectByUserId(AuthIdentity.Id);

            return Ok(expenseCategories);
        }

    }
}
