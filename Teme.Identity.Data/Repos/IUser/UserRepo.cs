using System.Linq;
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

        public async Task<AuthUser> GetByUsername(string username)
        {
            return await Repo.FirstOrDefaultAsync(x => x.UserName == username);
        }
    }
}