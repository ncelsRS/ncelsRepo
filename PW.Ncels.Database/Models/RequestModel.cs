using System.Collections.Generic;
using PW.Ncels.Database.DataModel;

namespace PW.Ncels.Database.Models
{
    public class RequestModel
    {
        public RequestModel()
        {
            AttachCodes=new List<string>();
        }
        public PriceProject Project { get; set; }

        public Organization HolderOrganization { get; set; }
        public Organization ProxyOrganization { get; set; }
        public Organization ManufacturerOrganization { get; set; }
        public DataModel.Price CurentPrice { get; set; }
        public DataModel.Price Price { get; set; }


        public RegisterProject Drug { get; set; }
        public Package Package { get; set; }
        public Composition Composition { get; set; }
        public Organization Manufacture { get; set; }
        public Organization Organization { get; set; }
        public Organization Applicant { get; set; }
        public Organization Export { get; set; }
        public Change Changes { get; set; }
        public Contract Contract { get; set; }
        public Contract HolderContract { get; set; }
        public Organization Holder { get; set; }
        public Organization Payer { get; set; }
        public Organization PayerTranslation { get; set; }
        public int StartDateYear { get; set; }
        public int StartDateMonth { get; set; }
        public int StartDateDay { get; set; }
        public int EndDateYear { get; set; }
        public int EndDateMonth { get; set; }
        public int EndDateDay { get; set; }
        public List<string> AttachCodes { get; set; }
    }
}