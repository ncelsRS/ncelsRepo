using System;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Teme.Shared.Data.Context;
using Teme.Shared.Data.Repos.IUser;

namespace Teme.Identity.Data.Repos.IUser
{
    public class UserRepo : BaseUserRepo, IUserRepo
    {
        public UserRepo(TemeContext context) : base(context)
        {
        }

        public async Task<AuthUser> GetLogin(Expression<Func<AuthUser, bool>> expression)
        {
            return await Repo
                .Where(expression)
                .Include(x => x.Scopes)
                .FirstOrDefaultAsync();
        }

    }
}