using Teme.Shared.Data.Context;
using Teme.Shared.Data.Repos;

namespace Teme.Shared.Logic
{
    public class BaseLogic : IBaseLogic
    {

    }

    public class BaseLogic<TIRepo, TEntity> : BaseLogic where TIRepo : IBaseRepo<TEntity> where TEntity : BaseEntity
    {
        protected TIRepo Repo { get; }

        protected BaseLogic(TIRepo repo)
        {
            Repo = repo;
        }
    }

}