using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Teme.Shared.Data.Context;
using Teme.Shared.Data.Repos.IUser;

namespace Teme.Identity.Data.Repos.IUser
{
    public interface IUserRepo : IBaseUserRepo
    {
        Task<AuthUser> GetLogin(Expression<Func<AuthUser, bool>> expression);
    }
}