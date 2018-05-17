using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Teme.Shared.Data.Context;

namespace Teme.Shared.Data.Repos.RefBaseRepo
{
    public interface IRefBaseRepo<TEntity> : IBaseRepo<TEntity> where TEntity : BaseEntity
    {

    }
}
