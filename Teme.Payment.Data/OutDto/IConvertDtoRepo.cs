using System;
using System.Collections.Generic;
using System.Text;

namespace Teme.Payment.Data.OutDto
{
    public interface IConvertDtoRepo
    {
        PaymentDto ConvertEntityToPayment(Shared.Data.Context.Payment payment);
    }
}
