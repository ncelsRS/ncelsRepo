using System;
using System.Collections.Generic;
using System.Text;
using Teme.Shared.Data.Context;

namespace Teme.Shared.Data.Repos
{
    public class EntityRepo : BaseRepo<BaseEntity>, IEntityRepo
    {
        public EntityRepo(TemeContext context) : base(context)
        {
        }
    }
}
