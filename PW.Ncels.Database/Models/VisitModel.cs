using System;
using System.Collections.Generic;
using PW.Ncels.Database.DataModel;
using PW.Ncels.Database.Helpers;

namespace PW.Ncels.Database.Models
{
    public class VisitModel
    {
        public int VisitId { get; set; }
        public DateTime VisitDate { get; set; }

        public string VisitDateShort
        {
            get
            {
                return VisitDate.ToShortDateString();
            }
        }

        public int VisitDuration { get; set; }
        public int VisitTimeBegin { get; set; }

        public string VisitInfo
        {
            get
            {
                return VisitDate.AddMinutes(VisitTimeBegin).ToString("yyyy.MM.dd HH:mm") + " (" + VisitDuration + ")";
            }
        }

        public string VisitComment { get; set; }
        public string VisitorLastName { get; set; }
        public string VisitorFirstName { get; set; }
        public string VisitorMiddleName { get; set; }

        public string VisitorName
        {
            get
            {
                return VisitorLastName + " " + VisitorFirstName + " " + VisitorMiddleName;
            }
        }
        public int VisitStatusId { get; set; }
        public string VisitStatusName { get; set; }
        public string VisitTypeName { get; set; }
        public string VisitTypeGroup { get; set; }

        public string VisitTypeFullName
        {
            get
            {
                return VisitTypeName + "(" + VisitTypeGroup + ")";
            }
        }

        public int? VisitRatingValue { get; set; }
        public string VisitRatingComment { get; set; }
    }
}