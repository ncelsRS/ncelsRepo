using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Teme.Contract.Data.DTO;

namespace Teme.Template.Logic
{
    interface IFillTemplateLogic
    {
        void FillContract(Dictionary<string, string> dictionary, Shared.Data.Context.Contract contract);
    }
}
