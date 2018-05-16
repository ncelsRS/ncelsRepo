using System;
using System.Collections.Generic;
using System.Text;

namespace RSC.Core.Model
{
    public class EmailModel
    {
        // Заголовок
        public string Subject { get; set; }
        //получатель
        public string To { get; set; }
        //текст
        public string Body { get; set; }
    }
}

