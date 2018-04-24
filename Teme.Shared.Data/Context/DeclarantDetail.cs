using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Teme.Shared.Data.Context
{
    public class DeclarantDetail : BaseEntity
    {
        public virtual ICollection<Contract> DeclarantDetailContract { get; set; }
        public virtual ICollection<Contract> ManufacturDetailContract { get; set; }
        public virtual ICollection<Contract> PayerDetailContract { get; set; }
    }
}
