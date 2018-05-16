using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PW.Ncels.Models
{
    public class EmailTemplateModel
    {
        public int Type { get; set; }
        public string Title { get; set; }
        public string Message { get; set; }
        public string Url { get; set; }
        public string HeadMessage { get; set; }
    }
}