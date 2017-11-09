using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PW.Ncels.Database.Models.Expertise
{
    public class DialogParameter
    {
        public Guid Id { get; set; }
        public string ContainerId { get; set; }
        public int Type { get; set; }
    }
}
