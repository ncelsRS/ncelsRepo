using System;
using System.Collections.Generic;
using System.Text;

namespace Teme.Declaration.Data.Model
{
    public class DeclarationUpdateModel
    {
        public int Id { get; set; }
        public string ClassName { get; set; }
        public object Fields { get; set; }
    }
}
