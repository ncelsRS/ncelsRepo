using System;
using System.Collections.Generic;
using System.Text;
using Teme.Shared.Data.Context;
using Teme.Shared.Data.Repos.RefBaseRepo;

namespace Teme.Common.Data.PackagingRepo
{
    public interface IPackagingRepo<TEntity> : IRefBaseRepo<TEntity> where TEntity : BaseEntity
    {
    }
}
