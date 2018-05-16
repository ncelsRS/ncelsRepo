using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PW.Ncels.Database.Models
{
    public class RejectDialogViewModel
    {
        public System.Guid Id { get; set; }
        public string Text { get; set; }
        public string Comment { get; set; }
        public string Url { get; set; }
        public string Payload { get; set; }
    }
}