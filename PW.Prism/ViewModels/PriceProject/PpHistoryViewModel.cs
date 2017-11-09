using System;
using System.Collections.Generic;
using System.Linq;
using PW.Ncels.Database.DataModel;

namespace PW.Prism.ViewModels.PriceProject{

    public class PpHistoryViewModel {
        public Ncels.Database.DataModel.PriceProject Project;
        public PriceProject_Ext ProjectExt;
        public Price CurrentPrice;
        public Price ExtPrice;
        public Organization ManufacturerOrganization;
        public Organization ProxyOrganization;
    }
}