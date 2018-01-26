using Kendo.Mvc.UI;
using PW.Ncels.Database.Constants;
using PW.Ncels.Database.DataModel;
using PW.Ncels.Database.Helpers;
using System;
using System.Linq;

namespace PW.Prism.ViewModels.OBK.OP
{
    public class AssessmentProgramVM
    {
        public AssessmentProgramVM() { }

        public AssessmentProgramVM(OBK_AssessmentDeclarationProgram program)
        {
            if (program != null)
            {
                DateFrom = program.DateFrom;
                DateTo = program.DateTo;
            }
        }
        public Guid Id { get; set; }
        public Guid DeclarationId { get; set; }
        public DateTime? DateFrom { get; set; }
        public DateTime? DateTo { get; set; }
        public string StatusCode { get; set; }
        public bool IsExecutor { get; set; }
        public DataSourceResult Attach { get; set; }

        public OBK_AssessmentDeclarationProgram ToDB()
        {
            return new OBK_AssessmentDeclarationProgram
            {
                Id = Id,
                DateFrom = DateFrom.Value,
                DateTo = DateTo.Value,
                DeclarationId = DeclarationId
            };
        }

    }

    public static partial class Extensions
    {
        public static AssessmentProgramVM ToVM(this OBK_AssessmentDeclarationProgram program)
        {
            return new AssessmentProgramVM(program);
        }
    }
}