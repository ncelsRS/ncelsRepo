using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PW.Ncels.Models
{
    public class MessageModel
    {
        public string ErrorText { get; set; }
        public bool IsError { get; set; }
        public bool IsNew { get; set; }
        public string Message { get; set; }
        public string Email { get; set; }
        public object Result { get; set; }
    }
}