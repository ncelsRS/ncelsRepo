using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PW.Prism.ViewModels.Expertise
{
    public class ExpertiseEntity
    {
        public Guid Guid { get; set; }
        public int DicStageId { get; set; }        
        public int NewInQueueCount { get; set; }
        public int InWorkCount { get; set; }
        public int NewInWorkCount { get; set; }
    }
}