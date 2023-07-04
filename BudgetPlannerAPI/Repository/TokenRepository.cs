using Common.Models;

using Repository.Contracts;

namespace Repository
{
    public class TokenRepository : RepositoryBase<Token>, ITokenRepository
    {
        public TokenRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {
        }

        public Token CreateToken(Token token)
        {
            return Create(token);
        }

        public IEnumerable<Token> SelectActiveByUserId(Guid userId, bool trackChanges = false)
        {
            return FindByCondition(token => token.UserId == userId && token.Active, trackChanges);
        }

        public Token? SelectById(Guid tokenId, bool trackChanges = false)
        {
            return FindByCondition(token => token.TokenId == tokenId, trackChanges).FirstOrDefault();
        }

        public Token? SelectByRefreshToken(string refreshToken, bool trackChanges = false)
        {
            return FindByCondition(token => token.RefreshToken == refreshToken, trackChanges).FirstOrDefault();
        }

        public IEnumerable<Token> SelectByUserId(Guid userId, bool trackChanges = false)
        {
            return FindByCondition(token => token.UserId == userId, trackChanges);
        }
    }
}
