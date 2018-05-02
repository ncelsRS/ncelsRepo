using Teme.Shared.Data.Context;
using Teme.Shared.Logic;

namespace Teme.Identity.Logic
{
    public interface IIdentityLogic : IBaseLogic
    {
        string GenerateToken(AuthUser user);
        string GenerateUpdateToken(int id);
        int RealiseUpdateToken(string token);
    }
}