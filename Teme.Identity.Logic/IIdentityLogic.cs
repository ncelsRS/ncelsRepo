using System.Collections.Generic;
using System.Threading.Tasks;
using Teme.Shared.Data.Context;
using Teme.Shared.Logic;

namespace Teme.Identity.Logic
{
    public interface IIdentityLogic : IBaseLogic
    {
        string GenerateOneTimeToken(int userId);
        string GenerateAccessToken(int userId, IEnumerable<string> audiences);
        string GenerateRefreshToken(int userId);
    }
}