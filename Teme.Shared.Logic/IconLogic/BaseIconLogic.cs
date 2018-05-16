using System;
using System.Collections.Generic;
using System.Text;
using Teme.Shared.Data.Context;
using Teme.Shared.Data.Repos.IconRepo;

namespace Teme.Shared.Logic.IconLogic
{
    public class BaseIconLogic<TIRepo> : BaseLogic<TIRepo, Icon>, IBaseIconLogic where TIRepo : IBaseIconRepo
    {
        protected BaseIconLogic(TIRepo repo) : base(repo)
        {
        }
    }
}
