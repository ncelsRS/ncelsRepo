using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PW.Prism.ViewModels.DrugDeclaration
{
    public class DeclarationStepsViewModel
    {
        public List<DeclarationStepsModel> Steps { get; set; }
        public Guid DeclarationId { get; set; }
    }
}