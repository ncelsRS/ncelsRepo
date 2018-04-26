using System;
using System.Collections.Generic;
using System.Text;

namespace Teme.Contract.Data.Model
{
    public class ContractUpdateModel
    {
        public int Id { get; set; }
        public string ClassName { get; set; }
        public object Fields { get; set; }
    }
}
