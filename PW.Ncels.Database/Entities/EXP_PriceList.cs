using PW.Ncels.Database.Constants;

namespace PW.Ncels.Database.DataModel
{
    using System;
    using System.Collections.Generic;

    public partial class EXP_PriceList
    {

        public string Name
        {
            get { return NameRu; }
        }

        public decimal Price
        {
            get { return PriceReRegisterKzNds ?? 0; }
        }


        public bool IsSelect { get; set; }


        public decimal GetPrice(string countryCode, string typeCode)
        {
            if (countryCode == CountryCodeConts.KZ)
            {
                if (typeCode == TypeCodeConts.Registration)
                {
                    var priceRegisterKzNds = this.PriceRegisterKzNds;
                    if (priceRegisterKzNds != null) return priceRegisterKzNds.Value;
                }
                if (typeCode == TypeCodeConts.ReRegistration)
                {
                    var priceReRegisterKzNds = this.PriceReRegisterKzNds;
                    if (priceReRegisterKzNds != null) return priceReRegisterKzNds.Value;
                }
            }

            if (typeCode == TypeCodeConts.Registration)
            {
                var priceRegisterForeignNds = this.PriceRegisterForeignNds;
                if (priceRegisterForeignNds != null) return priceRegisterForeignNds.Value;
            }
            if (typeCode == TypeCodeConts.ReRegistration)
            {
                var priceReRegisterForeignNds = this.PriceReRegisterForeignNds;
                if (priceReRegisterForeignNds != null) return priceReRegisterForeignNds.Value;
            }
            return 0;
        }
    }
}