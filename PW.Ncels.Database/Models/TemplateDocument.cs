using System;
using System.Collections.Generic;
using System.Linq;
using PW.Ncels.Database.DataModel;
using PW.Ncels.Database.Helpers;

namespace PW.Ncels.Database.Models
{
    public class TemplateDocument
    {
        public TemplateDocument(Document document)
        {
            var entities = UserHelper.GetCn();
            Document = document;
            CurrentEmployee = UserHelper.GetCurrentEmployee();
            ItemsValue = new Dictionary<string, string>();
            CommentValue = new Dictionary<string, string>();

            Employee signer = null;

            if (!string.IsNullOrEmpty(Document.SignerId))
                signer = entities.Employees.FirstOrDefault(o => o.Id == new Guid(Document.SignerId));

            if (signer != null)
            {
                ItemsValue.Add("Signer", GetSignerName(signer));
                ItemsValue.Add("SignerPosition", signer.Position.Name);
            }

            var items = DictionaryHelper.GetItems(document.AnswersId, document.AnswersValue);
            Guid priceProjectId;
            if (Guid.TryParse(document.AnswersId, out priceProjectId))
            {
                var obj = entities.ProjectsViews.FirstOrDefault(m => m.Id == priceProjectId);
                if (obj != null && obj.IsRegisterProject.HasValue)
                {
                    if (obj.IsRegisterProject.Value)
                    {
                        ItemsValue.Add("DrugInfo", "");
                    }
                    else
                    {
                        var drugItem = entities.PriceProjectsViews.FirstOrDefault(m => m.Id == priceProjectId);
                        string[] drugInfoItems = drugItem != null ? new[]
                        {
                            drugItem.NameRu, drugItem.FormNameRu,
                            drugItem.Dosage, (string.IsNullOrEmpty(drugItem.CountPackage)?"":"№"+drugItem.CountPackage),
                            drugItem.Concentration, drugItem.RegNumber, (drugItem.RegDate!=null?drugItem.RegDate.Value.ToString("dd.MM.yyyy"):"")
                        } : new string[0];
                        ItemsValue.Add("DrugInfo", string.Join(", ", drugInfoItems.Where(e => !string.IsNullOrEmpty(e))));
                    }
                }
            }
            ItemsValue.Add("LetterNumber", document.OutgoingNumber);
            ItemsValue.Add("LetterDate", document.DocumentDate.HasValue ? document.DocumentDate.Value.ToString("dd.MM.yyyy") : string.Empty);

            if (items.Count > 0)
            {
                var item = items.First();
                var id = new Guid(item.Id);
                var doc = entities.Documents.Find(id);
                ItemsValue.Add("OutgoingNumber", doc.OutgoingNumber);
                ItemsValue.Add("OutgoingDate",
                    doc.OutgoingDate.HasValue ? doc.OutgoingDate.Value.ToString("dd.MM.yyyy") : string.Empty);
            }
            ItemsValue.Add("CurrentDate", DateTime.Now.ToString("dd.MM.yyyy"));
            ItemsValue.Add("CorrespondentsValue", Document.CorrespondentsValue);
            ItemsValue.Add("ExecutorsValue", Document.ExecutorsValue);
            ItemsValue.Add("CorrespondentsInfo", Document.CorrespondentsInfo);
            ItemsValue.Add("Note", Document.Summary);
            ItemsValue.Add("CurrentEmployeeName", CurrentEmployee.ShortName);
            ItemsValue.Add("CurrentEmployeePhone", CurrentEmployee.Phone);
            ItemsValue.Add("CurrentEmployeeDepartment", CurrentEmployee.Position.Parent.Name);
            ItemsValue.Add("CurrentEmployeeEmail", CurrentEmployee.Email);


            var reports =
                entities.Reports.Where(o => o.TaskId == document.MainTaskId).OrderBy(o => o.ModifiedDate).ToList();

            for (var j = 1; j <= 4; j++)
                ItemsValue.Add("Comment" + j, reports.Count >= j ? reports[j - 1].Text : string.Empty);
        }

        public TemplateDocument()
        {
            //ItemsSource = new Dictionary<string, string>();
            //ItemsSource.Add(LocStr.Signer, "Signer");
            //ItemsSource.Add(LocStr.To1, "CorrespondentsInfo");
            //ItemsSource.Add(LocStr.PositionSigner, "SignerPosition");
            //ItemsSource.Add(LocStr.Recipient1, "CorrespondentsValue");
            //ItemsSource.Add(LocStr.Summary1, "Note");
            //ItemsSource.Add(LocStr.EmployeeName, "CurrentEmployeeName");
            //ItemsSource.Add(LocStr.EmployeePhone, "CurrentEmployeePhone");
            //ItemsSource.Add(LocStr.DepartmentExecutive, "CurrentEmployeeDepartment");
        }

        public Dictionary<string, string> CommentValue { get; set; }
        public Dictionary<string, string> ItemsValue { get; set; }
        public Dictionary<string, string> ItemsSource { get; set; }
        private Employee CurrentEmployee { get; }
        private Document Document { get; }

        private static string GetSignerName(Employee employee)
        {
            if (!string.IsNullOrEmpty(employee.FirstName) && employee.FirstName.Length > 0 &&
                !string.IsNullOrEmpty(employee.LastName))
                return string.Format("{0}. {1}", employee.FirstName[0], employee.LastName);
            return employee.ShortName;
        }
    }
}