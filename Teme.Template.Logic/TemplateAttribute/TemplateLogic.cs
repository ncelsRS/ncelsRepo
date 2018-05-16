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
using Teme.Template.Data.TemplateRepo;

namespace Teme.Template.Logic
{
    public class TemplateLogic : ITemplateLogic
    {
        private IFillTemplateLogic _fillTemplateLogic;
        private ITemplateRepo _templateRepo;
        public TemplateLogic(FillTemplateLogic fillTemplateLogic, TemplateRepo templateRepo)
        {
            _fillTemplateLogic = fillTemplateLogic;
            _templateRepo = templateRepo;
        }

        public void FillContractTemplate(Dictionary<string, string> dictionary, Document doc)
        {
            var replaceOptions = new FindReplaceOptions();
            replaceOptions.MatchCase = true;
            replaceOptions.FindWholeWordsOnly = true;
            foreach (KeyValuePair<string, string> dic in dictionary)
            {
                doc.Range.Replace(dic.Key, dic.Value, replaceOptions);
            }
        }

        public async Task<Dictionary<string, string>> FillDictionary(int id)
        {
            Dictionary<string, string> dictionary = new Dictionary<string, string>();
            var contract = await _templateRepo.GetContract(id);
            if (contract != null)
            {
                await _fillTemplateLogic.FillContract(dictionary, contract);
            }
            return dictionary;
        }

        public async Task<Stream> GetContract(int id)
        {
            var stream = new FileStream(@"wwwroot\Templates\ContractTemplateIMN_MT.docx", FileMode.Open);
            var doc = new Document(stream);
            stream.Close();

            FillContractTemplate(await FillDictionary(id), doc);

            //Работа с таблицой
            //var firstTable = (Table)doc.GetChild(NodeType.Table, 1, true);

            //Row r = new Row(doc);
            //Cell c = new Cell(doc);
            //Paragraph p = new Paragraph(doc);
            //Run currentRun = new Run(doc, "Content");
            //p.AppendChild(currentRun);
            //c.AppendChild(p);
            //r.AppendChild(c);
            //firstTable.Rows.Add(r);

            var newStream = new MemoryStream();
            doc.Save(newStream, SaveFormat.Pdf);
            newStream.Position = 0;

            return newStream;
        }

    }
}
