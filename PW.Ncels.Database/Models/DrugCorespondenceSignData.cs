using System;

namespace PW.Ncels.Database.Models
{
    public class DrugCorespondenceSignData
    {
        public Guid LetterId { get; set; }
        public string LetterNumber { get; set; }
        public DateTime LetterDate { get; set; }
    }
}