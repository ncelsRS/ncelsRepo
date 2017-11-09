using System;
using System.Collections.Generic;
using PW.Ncels.Database.DataModel;
using PW.Ncels.Database.Helpers;

namespace PW.Ncels.Database.Models
{
    public class VisitTypeModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string CategoryName { get; set; }
        public int Time { get; set; }
    }
}