using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PW.Ncels.Models
{
    public class CommingLetterFromEdo
    {
        public string id_edo { get; set; }
        public string letter_obk { get; set; }
        public string id_letter_edo { get; set; }
        public string date_letter_edo { get; set; }
        public string user_edo { get; set; }
        public string letter_text { get; set; }

        public List<CommingAttachFileFromEdo> attachments { get; set; }
    }
}