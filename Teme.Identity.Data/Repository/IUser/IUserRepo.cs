using Teme.Shared.Data.Context;
using Teme.Shared.Data.Repos.IUser;

namespace Teme.Identity.Data.Repository.IUser
{
    public interface IUserRepo : IBaseUserRepo
    {
        void AddUser(AuthUser authUser);
    }
}