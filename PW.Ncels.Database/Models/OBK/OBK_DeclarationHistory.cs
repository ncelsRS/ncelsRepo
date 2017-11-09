using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PW.Ncels.Database.Models.OBK
{
    public class OBK_DeclarationHistory
    {
        public Guid AssessmentDeclarationId { get; set; }
        public int StageId { get; set; }
        public int StageStatusId { get; set; }
        public string StageCode { get; set; }
        public string StageName { get; set; }
        public int StatusId { get; set; }
        public string StatusName { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string StartDateHistory { get; set; }
        public string EndDateHistory { get; set; }
        public string Note { get; set; }
    }
}
