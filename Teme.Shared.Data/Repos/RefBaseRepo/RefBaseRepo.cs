using System;
using System.Collections.Generic;
using System.Text;
using Teme.Shared.Data.Context;

namespace Teme.Shared.Data.Repos.RefBaseRepo
{
    public class RefBaseRepo<TEntity> : BaseRepo<TEntity>, IRefBaseRepo<TEntity> where TEntity : BaseEntity
    {
        public RefBaseRepo(TemeContext context) : base(context) { }
    }
}
