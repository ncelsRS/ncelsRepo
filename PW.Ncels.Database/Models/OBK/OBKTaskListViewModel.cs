using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PW.Ncels.Database.Models.OBK
{
    public class OBKTaskListViewModel
    {
        public Guid TaskId { get; set; }
        public string TaskNumber { get; set; }
        public string RegisterDate { get; set; }
        /// <summary>
        /// создатель задания в УОБК
        /// </summary>
        public string ExecutorName { get; set; }
        /// <summary>
        /// исполнитель в ЦОЗ приняыший образцы
        /// </summary>
        public string CozExecutorName { get; set; }
        /// <summary>
        /// исполнитель в ЦОЗ время передачи в ИЦ
        /// </summary>
        public string SentToIC { get; set; }
        /// <summary>
        /// Кому передано в ИЦ
        /// </summary>
        public string ICExecutorName { get; set; }
        public string ActNumber { get; set; }
        //public string LaboratoryTypeName { get; set; }
        public string UnitName { get; set; }
        /// <summary>
        /// в случае если акт отбора (заполненный экспертом)
        /// </summary>
        public bool CheckBoxIsVisible { get; set; }

        public List<OBKProductViewModel> ProductListViewModels { get; set; }
    }
}