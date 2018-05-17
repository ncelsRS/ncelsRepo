using System;
using System.Collections.Generic;
using System.Text;
using Teme.Shared.Data.Context;
using Teme.Shared.Data.Repos.RefBaseRepo;

namespace Teme.Common.Data.PackagingRepo
{
    public class PackagingRepo : RefBaseRepo<PaymentPackaging>, IPackagingRepo<PaymentPackaging>
    {
        public PackagingRepo(TemeContext context) : base(context)
        {
        }
    }
}
