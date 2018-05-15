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
        Stream GetContract();

        Task FillContractTemplate(Document doc, int id);
    }
}
