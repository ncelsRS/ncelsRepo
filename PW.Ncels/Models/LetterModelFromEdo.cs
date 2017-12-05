using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PW.Ncels.Models
{
    public class LetterModelFromEdo
    {
        public long ID { get; set; }
        public string nomerDogovora { get; set; }
        public string senderOrg { get; set; }
        public string ObkLetterNumber { get; set; }
        public string IdEdo { get; set; }
        public Nullable<long> LetterPortalEdoID { get; set; }
        public string LetterRegNomer { get; set; }
        public Nullable<System.DateTime> LetterRegDate { get; set; }
        public string UserEdo { get; set; }
        public string LetterText { get; set; }
    }
}