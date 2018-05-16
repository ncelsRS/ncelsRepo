using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PW.Ncels.Database.Models
{
    public class TmcOutViewModel
    {
        public System.Guid ? Id { get; set; }
        public System.Guid ? TmcOutId { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public System.Guid ? CreatedEmployeeId { get; set; }
        public int StateType { get; set; }
        public string StateTypeValue { get; set; }
        public string Note { get; set; }
        public Nullable<System.Guid> OutTypeDicId { get; set; }
        public string OutTypeDicValue { get; set; }
        public Nullable<System.Guid> StorageDicId { get; set; }
        public string StorageDicValue { get; set; }
        public string Safe { get; set; }
        public string Rack { get; set; }
        public Nullable<System.Guid> OwnerEmployeeId { get; set; }
        public string OwnerEmployeeValue { get; set; }
        public string Comment { get; set; }
    }
}
