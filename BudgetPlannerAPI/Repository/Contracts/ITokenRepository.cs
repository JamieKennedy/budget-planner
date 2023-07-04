using Common.Models;

namespace Repository.Contracts
{
    public interface ITokenRepository
    {
        Token CreateToken(Token token);
        Token? SelectById(Guid tokenId, bool trackChanges = false);
        Token? SelectByRefreshToken(string refreshToken, bool trackChanges = false);
        IEnumerable<Token> SelectByUserId(Guid userId, bool trackChanges = false);
        IEnumerable<Token> SelectActiveByUserId(Guid userId, bool trackChanges = false);
    }
}
