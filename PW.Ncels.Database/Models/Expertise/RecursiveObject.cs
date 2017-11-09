using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PW.Ncels.Database.Models.Expertise;
    
namespace PW.Ncels.Database.DataModel
{

    public class RecursiveObject
    {
        public string text { get; set; }
        public string title { get; set; }
        public string type { get; set; }
//        public string icon { get; set; }
        public Int64 id { get; set; }
        public FlatTreeAttribute state { get; set; }
        public List<RecursiveObject> children { get; set; }
    }
    public class FlatTreeAttribute
    {
        public bool selected;
        public bool opened;
        public bool disabled;
    }

    public class CheckBoxObject
    {
        public Guid ObjectId { get; set; }
        public string NameCheckBox { get; set; }
        public bool? IsCheckBox { get; set; }
    }
}
