using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PW.Ncels.Models.Visit
{
    public class TimeListModels
    {
        public List<VisitInterval> Intervals {get;set;}
    }

    public class VisitInterval
    {
        public DateTime From { get; set; }
        public DateTime To { get; set; }
        public Guid EmployeeId { get; set; }
        public bool IsExistsVisit { get; set; }

    }
}
