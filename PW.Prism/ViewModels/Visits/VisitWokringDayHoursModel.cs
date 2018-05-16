using System;
using PW.Ncels.Database.DataModel;
using System.Collections.Generic;

namespace PW.Prism.ViewModels.Visits
{
    public class VisitWokringDayHoursModel
    {
        public DateTime Date { get; set; }
        public List<WorkingDayInterval> Intervals { get; set; }
    }

    public class WorkingDayInterval
    {
        public int Id { get; set ;}
        public int From { get; set ; }
        public int To { get; set ; }
    }
}