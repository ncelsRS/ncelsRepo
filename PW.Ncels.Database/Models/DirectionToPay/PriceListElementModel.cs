using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PW.Ncels.Database.Models.DirectionToPay
{
    public class PriceListElementModel
    {
        public Guid Id { get; set; }

        public string Number { get; set; }

        private string _name;

        public string Name
        {
            get { return $"{Number}-{_name}({Price})"; }
            set { _name = value; }
        }

        public decimal? Price { get; set; }
    }
}