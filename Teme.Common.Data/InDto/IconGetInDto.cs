using System;
using System.Collections.Generic;
using System.Text;
using Teme.Shared.Data.Primitives.Icon;

namespace Teme.Common.Data.InDto
{
    public class IconGetInDto
    {
        public ModuleTypeEnum ModuleType { get; set; }
        public int ObjectId { get; set; }
        public string FieldName { get; set; }
    }
}

