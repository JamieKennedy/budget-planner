namespace Common.DataTransferObjects.Savings
{
    public class UpdateSavingsDto
    {
        public string? Name { get; set; } = null;
        public string? Description { get; set; } = null;
        public decimal? Goal { get; set; } = null;
        public DateTime? GoalDate { get; set; } = DateTime.MinValue;
    }
}
