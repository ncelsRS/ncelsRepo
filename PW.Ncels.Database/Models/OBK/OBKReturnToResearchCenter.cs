using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PW.Ncels.Database.Models.OBK
{
    public class OBKReturnToResearchCenter
    {
        public Guid TaskmaterailId { get; set; }
        public Guid ResearchCenterId { get; set; }
        public Guid TaskExecutorId { get; set; }
    }
}
