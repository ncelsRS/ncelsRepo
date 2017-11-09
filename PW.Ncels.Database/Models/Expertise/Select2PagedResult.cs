using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PW.Ncels.Database.Models.Expertise
{
    public class Select2PagedResult
    {
        public int Total { get; set; }
        public List<Select2Result> Results { get; set; }
    }
    public class Select2Result
    {
        public string id { get; set; }
        public string text { get; set; }
    }

    public class TermSearch
    {
        public string Id { get; set; }
        public string Term { get; set; }
    }

    public class BoooleanEntity
    {
        public bool IsSign { get; set; }
        public string NameRu { get; set; }
    }

 
    public class SubUpdateField
    {
        public string ModelId { get; set; }
        public long SubModelId { get; set; }
        public long RecordId { get; set; }
        public string ControlId { get; set; }

    }
}
