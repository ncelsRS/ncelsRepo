using Aspose.Words;
using Aspose.Words.Replacing;
using Aspose.Words.Tables;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Teme.Contract.Data;
using Teme.Contract.Data.DTO;
using Teme.Contract.Logic;

namespace Teme.Template.Logic
{
    public class TemplateLogic : ITemplateLogic
    {
        private IContractRepo _contractRepo;
        public TemplateLogic(ContractRepo contractRepo)
        {
            _contractRepo = contractRepo;
        }

        public async Task FillContractTemplate(Document doc, int id)
        {
            Dictionary<string, string> dictionary = new Dictionary<string, string>();
            var contractDto = await _contractRepo.GetContract(id);
            if (contractDto != null)
            {
                var declarantDto = await _contractRepo.GetDeclarant((int)contractDto.DeclarantId);
            }
        }

        public Stream GetContract()
        {
            var stream = new FileStream(@"wwwroot\Templates\ContractTemplateIMN_MT.docx", FileMode.Open);

            Document doc = new Document(stream);
            stream.Close();

            var replaceOptions = new FindReplaceOptions();
            replaceOptions.MatchCase = true;
            replaceOptions.FindWholeWordsOnly = true;

            doc.Range.Replace("{tttDeclarantsOrgformIDttt}", "Ұлан", replaceOptions);
            doc.Range.Replace("ttSurnamett", "Байтұров", replaceOptions);

            var firstTable = (Table)doc.GetChild(NodeType.Table, 1, true);

            Row r = new Row(doc);
            Cell c = new Cell(doc);
            Paragraph p = new Paragraph(doc);
            Run currentRun = new Run(doc, "Content");
            p.AppendChild(currentRun);
            c.AppendChild(p);
            r.AppendChild(c);
            firstTable.Rows.Add(r);

            var newStream = new MemoryStream();
            doc.Save(newStream, SaveFormat.Pdf);
            newStream.Position = 0;

            return newStream;
        }
    }
}
