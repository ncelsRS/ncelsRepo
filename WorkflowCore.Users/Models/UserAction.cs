using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WorkflowCore.Users.Models
{
    public class UserAction
    {
        public object OutcomeValue { get; set; }
        public string AwaiterKey { get; set; }
        public IEnumerable<string> ExecutorsIds { get; set; }
        public object Data { get; set; }
    }
}
