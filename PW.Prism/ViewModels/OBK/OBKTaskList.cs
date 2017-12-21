using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PW.Prism.ViewModels.OBK
{
    public class OBKTaskList
    {
        public Guid Id { get; set; }
        public string TaskNumber { get; set; }
        public string RegisterDate { get; set; }
        public string TaskEndDate { get; set; }
        public string CreateTaskExecutor { get; set; }
        public string AcceptCozExecutor { get; set; }
    }
}