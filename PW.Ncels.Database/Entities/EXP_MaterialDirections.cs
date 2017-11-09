using System.ComponentModel.DataAnnotations;
using System.Web.Script.Serialization;

namespace PW.Ncels.Database.DataModel
{
    using System;
    using System.Collections.Generic;

    [MetadataType(typeof(EXP_MaterialDirectionMetadata))]
    public partial class EXP_MaterialDirections
    {
        public class EXP_MaterialDirectionMetadata
        {
            public System.Guid Id { get; set; }
            public Nullable<System.DateTime> CreateDate { get; set; }
            public Nullable<System.DateTime> DeleteDate { get; set; }
            public string Number { get; set; }
            public System.Guid DrugDeclarationId { get; set; }
            public System.Guid StatusId { get; set; }
            public Nullable<System.Guid> SendEmployeeId { get; set; }
            public Nullable<System.DateTime> SendDate { get; set; }
            public Nullable<System.Guid> ExecutorEmployeeId { get; set; }
            public Nullable<System.DateTime> ReceiveDate { get; set; }
            public Nullable<System.DateTime> RejectDate { get; set; }
            public string Comment { get; set; }

            [ScriptIgnore(ApplyToOverrides = true)]
            public virtual Dictionary Status { get; set; }

          /*  [ScriptIgnore(ApplyToOverrides = true)]
            public virtual Employee ExecuterEmp { get; set; }
*/
     /*       [ScriptIgnore(ApplyToOverrides = true)]
            public virtual Employee SenderEmp { get; set; }*/

            [ScriptIgnore(ApplyToOverrides = true)]
            public virtual EXP_DrugDeclaration EXP_DrugDeclaration { get; set; }
        }
    }
}
