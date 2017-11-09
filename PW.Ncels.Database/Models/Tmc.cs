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

        public static class TmcStatuses
        {
            /// <summary>
            /// Новый
            /// </summary>
            public const int New = 0;

            /// <summary>
            /// Принятый
            /// </summary>
            public const int Accepted = 1;

            /// <summary>
            /// Списанный
            /// </summary>
            public const int Writeoff = 2;
        }
    }

    public class MeasureType
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}

