using System.Threading.Tasks;
using Teme.Shared.Data.Context;
using Teme.Shared.Data.Repos.IUser;

namespace Teme.Identity.Data.Repos.IUser
{
    public interface IUserRepo : IBaseUserRepo
    {
        Task<AuthUser> GetByUsername(string username);
    }
}