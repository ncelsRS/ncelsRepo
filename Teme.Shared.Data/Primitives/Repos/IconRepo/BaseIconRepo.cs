using System;
using System.Collections.Generic;
using System.Text;
using Teme.Shared.Data.Context;

namespace Teme.Shared.Data.Repos.IconRepo
{
    public abstract class BaseIconRepo : BaseRepo<Icon>, IBaseIconRepo
    {
        protected BaseIconRepo(TemeContext context) : base(context)
        {
        }
    }
}

