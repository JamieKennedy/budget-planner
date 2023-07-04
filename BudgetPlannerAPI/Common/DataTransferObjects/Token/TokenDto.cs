namespace Common.DataTransferObjects.Token
{
    public class TokenDto
    {
        public required string AccessToken { get; init; }
        public required string RefreshToken { get; init; }
    }
}
