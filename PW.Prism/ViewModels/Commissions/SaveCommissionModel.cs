using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PW.Prism.ViewModels.Commissions
{
    public class SaveCommissionModel
    {
        public int? Id { get; set; }
        public int? Type { get; set; }
        public DateTime Date { get; set; }
        public int Kind { get; set; }
        public string Comment { get; set; }
        public bool? Complete { get; set; }
        public List<Unit> Units { get; set; }

        public class Unit
        {
            public Guid Id { get; set; }
            public int Type { get; set; }
        }
    }
}