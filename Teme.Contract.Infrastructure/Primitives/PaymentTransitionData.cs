using System;
using System.Collections.Generic;
using System.Text;

namespace Teme.Contract.Infrastructure.Primitives
{
    public class PaymentTransitionData
    {
            public int PaymentId { get; set; }
            public Dictionary<string, IEnumerable<string>> ExecutorsIds { get; set; }
            public object Value { get; set; }
            public Dictionary<string, bool> Agreements { get; set; }

    }
}
