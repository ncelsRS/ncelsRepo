using System;

namespace PW.Ncels.Database.DataModel
{
    public partial class EXP_ExpertisePharmaceuticalFinalDoc
    {
        public EXP_ExpertisePharmaceuticalFinalDoc()
        {
            
        }
        public EXP_ExpertisePharmaceuticalFinalDoc(EXP_ExpertisePharmaceuticalFinalDoc doc)
        {
            this.Id=Guid.Empty;
            this.DosageStageId = doc.DosageStageId;
            this.Indicator1_1 = doc.Indicator1_1;
            this.Indicator2_1 = doc.Indicator2_1;
            this.Indicator2_2 = doc.Indicator2_2;
            this.Indicator2_3 = doc.Indicator2_3;
            this.Indicator2_4 = doc.Indicator2_4;
            this.Indicator3_1 = doc.Indicator3_1;
            this.Indicator3_2 = doc.Indicator3_2;
            this.Indicator3_3 = doc.Indicator3_3;
            this.Indicator4 = doc.Indicator4;
            this.Indicator5 = doc.Indicator5;
            this.Indicator6 = doc.Indicator6;
            this.Indicator7 = doc.Indicator7;
            this.Indicator12 = doc.Indicator12;
            this.Indicator13_1 = doc.Indicator13_1;
            this.Indicator13_2 = doc.Indicator13_2;
            this.Indicator13_3 = doc.Indicator13_3;
            this.Indicator14 = doc.Indicator14;
            this.Indicator15 = doc.Indicator15;
            this.Indicator16 = doc.Indicator16;
            this.Indicator17 = doc.Indicator17;
            this.Indicator18 = doc.Indicator18;
            this.Indicator19 = doc.Indicator19;
            this.Indicator20 = doc.Indicator20;
            this.ExpertConslusion = doc.ExpertConslusion;
        }
    }
}