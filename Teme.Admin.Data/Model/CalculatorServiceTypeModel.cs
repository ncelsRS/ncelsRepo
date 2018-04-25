using System;
using System.Collections.Generic;
using System.Text;

namespace Teme.Admin.Data.Model
{
    public class CalculatorServiceTypeModel : ReferenceModel
    {
        public bool Children { get; set; }

        public IEnumerable<CalculatorServiceTypeModificationModel> CalculatorServiceTypeModificationModels { get; set; }
    }

    public class CalculatorServiceTypeModificationModel : ReferenceModel
    {

    }
}
