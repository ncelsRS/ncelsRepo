using System;
using PW.Ncels.Database.DataModel;
using System.Collections.Generic;

namespace PW.Prism.ViewModels.Visits
{
    public class VisitWokringTimeModel
    {
        public List<VisitTypeSettings> VisitTypes { get; set; }
    }

    public class VisitTypeSettings
    {
        public VisitType Type { get; set ; }
        public bool IsEnable { get; set ; }
    }
}