namespace PW.Ncels.Database.DataModel
{
    using System;
    using System.Collections.Generic;

    public partial class TmcReportTask
    {
        public string ReportName
        {
            get { return this.TmcReport.Name; }
        }

        public DateTime ReportCreateDate
        {
            get { return this.TmcReport.CreatedDate; }
        }
    }
}
