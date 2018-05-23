using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Teme.Shared.Data.Context;
using Teme.Shared.Data.Repos;

namespace Teme.Shared.Logic
{
    public class BaseLogic : IBaseLogic
    {
        /// <summary>
        /// Доступно только в логике с параметром типа
        /// </summary>
        /// <returns></returns>
        [Obsolete("Доступно только в логике с параметром типа")]
        public IQueryable<AuthUser> GetUser(int userId)
        {
            throw new System.NotImplementedException();
        }

    }

    public class BaseLogic<TIRepo, TEntity> : BaseLogic where TIRepo : IBaseRepo<TEntity> where TEntity : BaseEntity
    {
        protected TIRepo Repo { get; }

        public BaseLogic(TIRepo repo)
        {
            Repo = repo;
        }

        public new IQueryable<AuthUser> GetUser(int userId)
        {
            return Repo.GetCurrentUser(userId);
        }
    }

}
