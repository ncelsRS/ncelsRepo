using System;
using System.Collections.Generic;
using System.Text;

namespace Teme.Payment.Data.InDto
{
    public class PaymentUpdateModel
    {
        public int Id { get; set; }
        public string ClassName { get; set; }
        public object Fields { get; set; }
    }
}
