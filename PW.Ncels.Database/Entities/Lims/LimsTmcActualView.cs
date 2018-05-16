using PW.Ncels.Database.Interfaces;

namespace PW.Ncels.Database.DataModel
{
    using System;
    using System.Collections.Generic;

    public partial class LimsTmcActualView : ITmcRequest
    {
        public Guid? OwnerEmployeeId { get; set; }
    }
}