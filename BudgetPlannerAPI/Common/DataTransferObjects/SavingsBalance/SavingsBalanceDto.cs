using Newtonsoft.Json;

namespace Common.DataTransferObjects.SavingsBalance
{
    public class SavingsBalanceDto
    {
        [JsonProperty]
        public long SavingsBalanceId { get; set; }
        [JsonProperty]
        public long SavingsId { get; set; }
        [JsonProperty]
        public decimal Balance { get; set; }

        [JsonProperty]
        public DateTime Created { get; set; }
    }
}
