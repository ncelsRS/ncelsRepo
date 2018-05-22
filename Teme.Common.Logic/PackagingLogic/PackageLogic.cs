using System;
using System.Collections.Generic;
using System.Text;
using Teme.Common.Data.PackagingRepo;
using Teme.Shared.Data.Context;
using Teme.Shared.Logic.RefBaseLogic;

namespace Teme.Common.Logic.PackagingLogic
{
    public class PackageLogic : RefBaseLogic<IPackagingRepo<PaymentPackaging>, PaymentPackaging>, IPackagingLogic<PaymentPackaging>
    {
        public PackageLogic(IPackagingRepo<PaymentPackaging> repo) : base(repo){}
    }
}
