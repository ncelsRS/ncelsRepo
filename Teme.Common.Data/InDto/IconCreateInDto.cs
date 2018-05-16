using System;
using System.Collections.Generic;
using System.Text;
using Teme.Shared.Data.Primitives;

namespace Teme.Common.Data.InDto
{
    public class IconCreateInDto: IconGetInDto
    {
        //public ModuleTypeEnum ModuleType { get; set; }
        //public int ObjectId { get; set; }
        //public string FieldName { get; set; }
        public string ValueField { get; set; }
        public string DisplayField { get; set; }
        public string Note { get; set; }
    }
}
