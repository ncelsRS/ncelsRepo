namespace PW.Ncels.Database.DataModel
{
    using System;
    using System.Collections.Generic;

    public partial class Tmc
    {
       public MeasureType MeasureTypeConvertDic
       {
           get;
           set;
       }
    }

    public class MeasureType
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}

