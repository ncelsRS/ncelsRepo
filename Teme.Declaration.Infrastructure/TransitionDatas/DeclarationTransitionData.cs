using System;
using System.Collections.Generic;
using System.Text;

namespace Teme.Declaration.Infrastructure.TransitionDatas
{
    public class DeclarationTransitionData
    {
        public int DeclarationId { get; set; }
        public Dictionary<string, IEnumerable<string>> ExecutorsIds { get; set; }
        public object Value { get; set; }
        public Dictionary<string, bool> Agreements { get; set; }
    }
}
