using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PW.Ncels.Models.Visit
{
    public class VisitRatingModel
    {
        public string Comment { get; set; }
        public int VisitId { get; set; }
    }
}
