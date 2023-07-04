using Newtonsoft.Json;

namespace Common.DataTransferObjects.Authentication
{
    public class AuthenticationCookieDto
    {
        [JsonProperty]
        public required string RefreshToken { get; set; }
        [JsonProperty]
        public bool KeepLoggedIn { get; set; }
    }
}
