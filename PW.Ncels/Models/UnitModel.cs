using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PW.Ncels.Database.DataModel;

namespace PW.Ncels.Models
{
    public class UnitModel:Unit
    {
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
    }
}