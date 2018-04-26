using Teme.Shared.Data.Context;

namespace Teme.Shared.Data.Repos.IUser
{
    public class BaseUserRepo:BaseRepo<AuthUser>, IBaseUserRepo
    {
        protected BaseUserRepo(TemeContext context) : base(context)
        {
        }
    }
}