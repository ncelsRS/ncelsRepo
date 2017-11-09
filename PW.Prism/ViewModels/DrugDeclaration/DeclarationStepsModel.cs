using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PW.Prism.ViewModels.DrugDeclaration
{
    public class DeclarationStepsModel
    {
        public bool IsLeaderStep { get; set; }
        public Guid Id { get; set; }
        public string Status { get; set; }
        public bool NeedWorkers { get; set; }
        public string StepName { get; set; }
        public bool AllowAddWorkers { get; set; }
        public string ExecutorShortName { get; set; }
        public int? DueToAllDays { get; set; }
        public string DateStart { get; set; }
        public string DateEnd { get; set; }
        public string ControlDate { get; set; }
        public int? DueToEndDays { get; set; }
        public bool StepIsEnded { get; set; }
        public bool StepIsOverdue { get; set; }
        public int? OverdueDays { get; set; }
        public int PriorityInList { get; set; }
        public string StageCode { get; set; }
        public int StageId { get; set; }

    }
}