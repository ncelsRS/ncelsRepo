using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Teme.Shared.Data.Context;
using Teme.Shared.Data.Repos;
using Teme.Shared.Logic;

namespace Teme.Common.Logic
{
    public class ReferenceLogic : EntityLogic, IReferenceLogic
    {
        public ReferenceLogic(IEntityRepo repo) : base(repo)
        {
        }

        public async Task<object> GetGosRegistry(string name, string culture, int page, int counter)
        {
            return new { };
        }
    }
}
