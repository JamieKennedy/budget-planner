using API.Extensions;

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
            var expenseCategoryResult = (await serviceManager.ExpenseCategoryService.CreateExpenseCategory(AuthIdentity.Id, createExpenseCategoryDto)).WithCreated(nameof(GetExpenseCategory));



            return HandleResult(expenseCategoryResult);
        }

        [HttpPatch("{expenseCategoryId}", Name = nameof(UpdateExpenseCategory))]
        public async Task<IActionResult> UpdateExpenseCategory(Guid expenseCategroyId, [FromBody] UpdateExpenseCategoryDto updateExpenseCategoryDto)
        {
            var updatedExpenseCategoryResult = await serviceManager.ExpenseCategoryService.UpdateExpenseCategory(AuthIdentity.Id, expenseCategroyId, updateExpenseCategoryDto);

            return HandleResult(updatedExpenseCategoryResult);
        }

        [HttpDelete(Name = nameof(DeleteExpenseCategory))]
        public async Task<IActionResult> DeleteExpenseCategory(Guid expenseCategoryId)
        {
            var result = await serviceManager.ExpenseCategoryService.DeleteExpenseCategory(AuthIdentity.Id, expenseCategoryId);

            return HandleResult(result);
        }

        [HttpGet("{expenseCategoryId}", Name = nameof(GetExpenseCategory))]
        public async Task<IActionResult> GetExpenseCategory(Guid expenseCategoryId)
        {
            var expenseCategoryResult = await serviceManager.ExpenseCategoryService.SelectById(AuthIdentity.Id, expenseCategoryId);

            return HandleResult(expenseCategoryResult);
        }

        [HttpGet(Name = nameof(GetExpenseCategoryByUserId))]
        public async Task<IActionResult> GetExpenseCategoryByUserId()
        {
            var expenseCategoriesResult = await serviceManager.ExpenseCategoryService.SelectByUserId(AuthIdentity.Id);

            return HandleResult(expenseCategoriesResult);
        }

    }
}
