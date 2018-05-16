using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PW.Ncels.Database.Models.OBK
{
    public class OBKTaskComment
    {
        public Guid TaskExecutorId { get; set; }
        public Guid ResearchCenterId { get; set; }
        public List<TaskComments> TaskComment { get; set; }

    }

    public class TaskComments
    {
        public string AutorName { get; set; }
        public string Note { get; set; }
        public Nullable<System.DateTime> Createdate { get; set; }
    }
}
