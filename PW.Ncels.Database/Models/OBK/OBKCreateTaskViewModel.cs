using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PW.Ncels.Database.Models.OBK
{
    public class OBKCreateTaskViewModel
    {
        public Guid AssessmentDeclarationId { get; set; }
        public Guid ActReceptionId { get; set; }
        public string ActNumber { get; set; }
        public string TaskNumber { get; set; }
        public string RegisterDate { get; set; }
        public Guid UnitId { get; set; }

        public List<string> LaboratoryTypeIds { get; set; }

        public MultiSelectList LaboratoryTypeList { get; set; }


        public List<OBKProductViewModel> ProductViewModels { get; set; }
        public List<OBKTaskViewModel> TaskViewModels { get; set; }
        public List<OBKProductViewModel> ModalViewModels { get; set; }
    }
}