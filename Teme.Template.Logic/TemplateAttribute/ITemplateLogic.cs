using Aspose.Words;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Teme.Template.Logic
{
    public interface ITemplateLogic
    {
        Task<Stream> GetContract(int id);

        Task<Dictionary<string, string>> FillDictionary(int id);

        void FillContractTemplate(Dictionary<string, string> dictionary, Document doc);
    }
}
