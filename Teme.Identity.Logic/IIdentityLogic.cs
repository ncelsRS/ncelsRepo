using Teme.Shared.Data.Context;
using Teme.Shared.Logic;

namespace Teme.Identity.Logic
{
    public interface IIdentityLogic : IBaseLogic
    {
        string GenerateToken(AuthUser user, IdentityLogic.TokenType tokenType = IdentityLogic.TokenType.Access);
    }
}