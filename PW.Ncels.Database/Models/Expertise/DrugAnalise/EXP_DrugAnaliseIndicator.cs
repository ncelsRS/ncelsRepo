using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PW.Ncels.Database.DataModel
{
    public partial class EXP_DrugAnaliseIndicator 
    {
        public EXP_DrugAnaliseIndicator()
        {
            
        }
        public EXP_DrugAnaliseIndicator(EXP_DrugAnaliseIndicator doc)
        {
            this.Id = Guid.Empty;
            this.DosageStageId = doc.DosageStageId;
            this.AnalyseIndicator = doc.AnalyseIndicator;
            this.Temperature = doc.Temperature;
            this.Humidity = doc.Humidity;
            this.Designation = doc.Designation;
            this.Demand = doc.Demand;
            this.ActualResult = doc.ActualResult;
            this.IsMatches = doc.IsMatches;
            this.InProtocol = doc.InProtocol;
            this.PositionNumber = doc.PositionNumber;
        }
        public string IsMatchesStr
        {
            get
            {
                if (IsMatches == null)
                {
                    return null;
                }
                return IsMatches.Value ? "Да" : "Нет";
            }
        }

        public string AnalyseIndicatorName { get; set; }
        
    }
}
