using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PW.Ncels.Database.Models.EMP
{
    public class EMPRegister
    {
        public int Id { get; set; }
        public string RegNumberRu { get; set; }
        public string RegNumberKz { get; set; }
        public string RegNameRu { get; set; }
        public string RegNameKz { get; set; }
        public string RegProducerNameRu { get; set; }
        public string RegProducerNameKz { get; set; }
        public string RegCounrtyNameRu { get; set; }
        public string RegCounrtyNameKz { get; set; }
    }
}
