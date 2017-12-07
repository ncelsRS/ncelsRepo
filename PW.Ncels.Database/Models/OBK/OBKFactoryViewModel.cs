using System;

namespace PW.Ncels.Database.Models.OBK
{
    public class OBKFactoryViewModel
    {
        public Guid? Id { get; set; }
        public String Name { get; set; }

        public String LegalLocation { get; set; }

        public String ActualLocation { get; set; }

        public int Count { get; set; }
    }
}
