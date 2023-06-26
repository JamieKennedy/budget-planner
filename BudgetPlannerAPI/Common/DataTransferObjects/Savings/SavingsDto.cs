using Common.DataTransferObjects.SavingsBalance;

using Newtonsoft.Json;

namespace Common.DataTransferObjects.Savings
{
    public class SavingsDto
    {
        [JsonProperty]
        public long SavingsId { get; set; }
        [JsonProperty]
        public long UserId { get; set; }
        [JsonProperty]
        public string Name { get; set; } = string.Empty;
        [JsonProperty]
        public string Description { get; set; } = string.Empty;
        [JsonProperty]
        public decimal Goal { get; set; }
        [JsonProperty]
        public DateTime LastModified { get; set; }
        [JsonProperty]
        public DateTime Created { get; set; }
        [JsonProperty]
        public List<SavingsBalanceDto> SavingsBalances { get; set; } = new List<SavingsBalanceDto>();
    }
}
