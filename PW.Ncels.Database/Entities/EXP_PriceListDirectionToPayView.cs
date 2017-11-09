namespace PW.Ncels.Database.DataModel
{
    using System;
    using System.Collections.Generic;

    public partial class EXP_PriceListDirectionToPayView
    {
        private decimal PriceLocal
        {
            get { return PriceRegisterKzNds ?? 0; }
        }

        private Guid? _priceListId;
        public Guid? PriceListId
        {
            get
            {
                if (_priceListId == null && Id != Guid.Empty)
                    _priceListId = Id;
                return _priceListId;
            }
            set { _priceListId = value; }
        }
    }
}

