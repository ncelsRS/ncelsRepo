using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PW.Ncels.Models
{
    public class LetterModel
    {
        public long ID { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public Nullable<System.DateTime> LetterSentEdoDate { get; set; }
        public Nullable<System.Guid> AuthorID { get; set; }
        public Nullable<System.DateTime> LetterRegDate { get; set; }
        public string LetterContent { get; set; }
        public Nullable<int> LetterStatusId { get; set; }
        public Nullable<System.Guid> ContractId { get; set; }
        public string EdoRegNomer { get; set; }
        public Nullable<System.DateTime> EdoRegDate { get; set; }

        public string contractName { get; set; }
        public string nomerPisma { get; set; }

        public string RegName { get; set; }

        public List<AttachDoc> listDoc { get; set; }

        public HttpPostedFileBase[] files { get; set; }
    }
}