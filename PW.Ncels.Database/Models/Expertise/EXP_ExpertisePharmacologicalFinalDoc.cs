using System;

namespace PW.Ncels.Database.DataModel
{
    public partial class EXP_ExpertisePharmacologicalFinalDoc
    {
        public EXP_ExpertisePharmacologicalFinalDoc()
        {
            
        }
        public EXP_ExpertisePharmacologicalFinalDoc(EXP_ExpertisePharmacologicalFinalDoc doc)
        {
            this.Id = Guid.Empty;
            this.DosageStageId = doc.DosageStageId;
            this.Indicator1 = doc.Indicator1;
            this.Indicator2 = doc.Indicator2;
            this.Indicator3 = doc.Indicator3;
            this.Indicator4 = doc.Indicator4;
            this.Indicator5 = doc.Indicator5;
            this.Indicator6 = doc.Indicator6;
            this.Indicator7 = doc.Indicator7;
            this.Indicator8 = doc.Indicator8;
            this.Indicator9 = doc.Indicator9;
            this.Indicator10 = doc.Indicator10;
            this.Indicator11 = doc.Indicator11;
            this.Indicator12 = doc.Indicator12;
            this.Indicator13 = doc.Indicator13;
            this.Indicator14 = doc.Indicator14;
            this.Indicator15 = doc.Indicator15;
            this.Indicator16 = doc.Indicator16;
            this.Indicator17 = doc.Indicator17;
            this.Indicator18 = doc.Indicator18;
            this.Indicator19 = doc.Indicator19;
            this.Indicator20 = doc.Indicator20;
            this.Indicator21 = doc.Indicator21;
            this.Indicator22 = doc.Indicator22;
            this.Indicator23 = doc.Indicator23;

            this.ExpertConslusion = doc.ExpertConslusion;
        }
    }
}