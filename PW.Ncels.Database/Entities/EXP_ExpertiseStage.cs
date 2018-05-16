namespace PW.Ncels.Database.DataModel
{
    using System;
    using System.Collections.Generic;

    public partial class EXP_ExpertiseStage
    {
        public EXP_ExpertiseStage(bool isNew): this()
        {
            IsNew = isNew;
        }

        public string CurrentStage { get; set; }
    }
}
