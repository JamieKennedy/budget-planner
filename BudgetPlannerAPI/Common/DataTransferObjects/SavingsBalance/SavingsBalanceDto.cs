using Common.DataTransferObjects.Base;

using Newtonsoft.Json;

namespace Common.DataTransferObjects.SavingsBalance
{
    public class SavingsBalanceDto : DtoBase
    {

        [JsonProperty]
        public Guid SavingsId { get; set; }
        [JsonProperty]
        public decimal Balance { get; set; }


    }
}
