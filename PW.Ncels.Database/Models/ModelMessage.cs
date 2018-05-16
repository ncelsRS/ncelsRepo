using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PW.Ncels.Database.Models
{
    public class ModelMessage
    {
        public string ErrorText { get; set; }
        public bool IsError { get; set; }
        public bool IsNew { get; set; }
        public string Message { get; set; }
        public string Email { get; set; }

        public object Result { get; set; }
    }
}
