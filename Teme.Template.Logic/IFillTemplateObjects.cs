using System;
using System.Collections.Generic;
using System.Text;
using Teme.Contract.Data.DTO;

namespace Teme.Template.Logic
{
    interface IFillTemplateObjects
    {
        void FillContract(Dictionary<string, string> dictionary, Shared.Data.Context.Contract contract);
        void FillDeclarant(Dictionary<string, string> dictionary, Shared.Data.Context.Declarant declarant);
        void FillPayer(Dictionary<string, string> dictionary, Shared.Data.Context.Declarant payer);

    }
}
