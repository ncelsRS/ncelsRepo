using PW.Ncels.Database.Constants;

namespace PW.Ncels.Database.DataModel
{
    using System;
    using System.Collections.Generic;

    public partial class TmcReportsView
    {
        public string OperationStr
        {
            get
            {
                return OperatorConsts.GetDisplayText(this.Operation);
            }
        }
    }
}
