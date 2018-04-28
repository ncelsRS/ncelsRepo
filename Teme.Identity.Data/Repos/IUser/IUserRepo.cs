using System.Threading.Tasks;
using Teme.Shared.Data.Context;

namespace Teme.Identity.Data.Repos.IUser
{
    public interface IUserRepo
    {
        Task<AuthUser> GetByUsername(string username);
    }
}