using System;
using System.Collections.Generic;
using System.Text;
using Teme.Shared.Data.Primitives.Contract;

namespace Teme.Contract.Data.Model
{
    public class CreateModel
    {
        public ContractTypeEnum ContractType { get; set; }
        public string ContractScope { get; set; }
    }
}
