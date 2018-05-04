using System;
using System.Collections.Generic;
using System.Text;
using Teme.Shared.Data.Context;
using Teme.Shared.Data.Repos.PaymentRepo;

namespace Teme.Shared.Logic.PaymentLogic
{
    public class BasePaymentLogic<TIRepo> : BaseLogic<TIRepo, Payment>, IBasePaymentLogic where TIRepo : IBasePaymentRepo
    {
        protected BasePaymentLogic(TIRepo repo) : base(repo)
        {
        }
    }
}
