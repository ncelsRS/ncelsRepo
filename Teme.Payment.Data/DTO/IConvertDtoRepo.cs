using System;
using System.Collections.Generic;
using System.Text;

namespace Teme.Payment.Data.DTO
{
    public interface IConvertDtoRepo
    {
        PaymentDto ConvertEntityToPayment(Shared.Data.Context.Payment payment);
    }
}
