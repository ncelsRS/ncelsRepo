using Teme.Identity.Data.Repository.IUser;
using Teme.Shared.Logic.IUser;

namespace Teme.Identity.Logic.IUser
{
    public class UserLogic : BaseUserLogic<IUserRepo>, IUserLogic
    {
        protected UserLogic(IUserRepo repo) : base(repo)
        {
        }
    }
}