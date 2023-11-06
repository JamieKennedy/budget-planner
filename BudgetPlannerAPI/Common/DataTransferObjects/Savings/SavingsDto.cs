using Common.DataTransferObjects.Base;
using Common.DataTransferObjects.SavingsBalance;

using Newtonsoft.Json;

namespace Common.DataTransferObjects.Savings
{
    public class SavingsDto : DtoModifiableBase
    {

        [JsonProperty]
        public Guid UserId { get; set; }
        [JsonProperty]
        public string Name { get; set; } = string.Empty;
        [JsonProperty]
        public string Description { get; set; } = string.Empty;
        [JsonProperty]
        public decimal Goal { get; set; }
        [JsonProperty]
        public DateTime? GoalDate { get; set; }
        [JsonProperty]

        public List<SavingsBalanceDto> SavingsBalances { get; set; } = new List<SavingsBalanceDto>();
    }
}
