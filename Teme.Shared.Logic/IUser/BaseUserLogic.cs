using Teme.Shared.Data.Context;
using Teme.Shared.Data.Repos.IUser;

namespace Teme.Shared.Logic.IUser
{
    public class BaseUserLogic<TIRepo> : BaseLogic<TIRepo, AuthUser>, IBaseUserLogic where TIRepo : IBaseUserRepo
    {
        public BaseUserLogic(TIRepo repo) : base(repo)
        {
        }
    }
}