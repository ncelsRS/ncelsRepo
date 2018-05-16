using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PW.Ncels.Models.Visit
{
    public class VisitListModels
    {
        public VisitListTypes Type { get; set; }
        public string DirectoryText { get; set; }
    }

    public enum VisitListTypes
    {
        All = 1,
        Actual = 2,
        Archive = 3,
    }
}
