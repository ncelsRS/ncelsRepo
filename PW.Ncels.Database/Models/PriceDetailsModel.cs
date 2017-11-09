using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PW.Ncels.Database.DataModel;

namespace PW.Ncels.Database.Models {
    public class PriceDetailsModel
    {
		public Guid Id { get; set; }
        public OrganizationsView Manufaturer { get; set; }
        public OrganizationsView Holder { get; set; }
        public OrganizationsView Proxy { get; set; }
        public PriceProjectsView PriceProject { get; set; }
        public RegisterProjectsView Drug { get; set; }
        public PricesView CurrentPrice { get; set; }
        public PricesView Price { get; set; }
        public PackagesView Package { get; set; }
        public ContractsView Contract { get; set; }
        public bool IsEdit { get; set; }
    }
}