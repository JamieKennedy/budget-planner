﻿namespace Common.DataTransferObjects.ExpenseCategory
{
    public class ExpenseCategoryDto
    {
        public Guid ExpenseCategoryId { get; set; }
        public Guid UserId { get; set; }
        public string Name { get; set; }
        public string ColourHex { get; set; }
    }
}