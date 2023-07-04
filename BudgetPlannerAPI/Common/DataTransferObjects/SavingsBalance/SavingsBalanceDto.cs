using Newtonsoft.Json;

namespace Common.DataTransferObjects.SavingsBalance
{
    public class SavingsBalanceDto
    {
        [JsonProperty]
        public Guid SavingsBalanceId { get; set; }
        [JsonProperty]
        public Guid SavingsId { get; set; }
        [JsonProperty]
        public decimal Balance { get; set; }

        [JsonProperty]
        public DateTime Created { get; set; }
    }
}
