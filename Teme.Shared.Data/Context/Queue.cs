using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Teme.Shared.Data.Context
{
    public class Queue
    {
        [Required]
        public string Url { get; set; }
        [Required]
        public string RequestJson { get; set; }
        public string ResponseJson { get; set; }
        public DateTime SendDate { get; set; }       

    }
}
