using System;
using System.Collections.Generic;
using System.Linq;
using Stimulsoft.Report;

namespace PW.Ncels.Database.DataModel
{
    public partial class EXP_RegistrationExpSteps
    {
        private Guid? responsibleSubdivId;
        private List<Guid> executorsIds=new GuidList();

        public Guid? ResponsibleSubdivisionId
        {
            get
            {
                if (Executors != null && Executors.Count > 0) return Executors.FirstOrDefault().Position.Parent.Id;
                return responsibleSubdivId;
            }
            set { responsibleSubdivId = value; }
        }

        public List<Guid> ExecutorsIds
        {
            get
            {
                if (Executors != null && Executors.Count > 0) return Executors.Select(e => e.Id).ToList();
                return executorsIds;
            }
            set { executorsIds = value; }
        }
    }
}