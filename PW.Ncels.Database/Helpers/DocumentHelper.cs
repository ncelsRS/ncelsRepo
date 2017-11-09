using System;
using System.Collections.Generic;
using System.Linq;
using PW.Ncels.Database.DataModel;

namespace PW.Ncels.Database.Helpers
{
    public static class DocumentHelper
    {
        public static List<Document> GetFondDocuments(ncelsEntities db, int type) {
            Guid currentOrganizationId = UserHelper.GetCurrentEmployee().OrganizationId;
            Guid umcId = Guid.Parse("8f0b91f3-af29-4d3c-96d6-019cbbdfc8be");
            var documents = db.Documents
                .Join(db.Tasks, d => d.Id, t => t.DocumentId, (d, t) => new {t, d})
                .Join(db.Employees,te=>te.t.ExecutorId,e=>e.Id.ToString(),(te,e)=>new {te,e})
                .Where(m=>m.e.OrganizationId == currentOrganizationId 
                && m.te.d.OrganizationId == umcId
                && m.te.d.DocumentType==type && !m.te.d.IsDeleted)
                .Select(m => m.te.d).ToList();
            var result = documents.Distinct().OrderByDescending(m => m.Number);
            return result.ToList();
        }

        public static bool IsCreatorDocument(ncelsEntities db, Guid id) {
            var createdUserId = db.Documents.First(m => m.Id == id).CreatedUserId;
            return Guid.Parse(createdUserId) == UserHelper.GetCurrentEmployee().Id;
        }

        public static bool IsMainCorespondense(ncelsEntities db, Guid id)
        {
            var isMain = db.Documents.First(m => m.Id == id).ApplicantType == 1;
            return isMain;
        }

        public static bool IsToolBarAllow(ncelsEntities db, Guid id) {
            var isAllow = (IsCreatorDocument(db, id) && !IsMainCorespondense(db, id)) || (IsMainCorespondense(db, id) && EmployePermissionHelper.IsMenuInnerDocVisibility && IsCurrentOrganizationDocument(db, id));
            return isAllow;
        }

        public static bool IsCurrentOrganizationDocument(ncelsEntities db, Guid id)
        {
            Guid organizationId = UserHelper.GetCurrentEmployee().OrganizationId;
            var isCurrent = db.Documents.First(m => m.Id == id).OrganizationId == organizationId;
            return isCurrent;
        }


        public static void DeleteSignTask(ncelsEntities db, Document document)
        {
            var signActivity = db.Activities.FirstOrDefault(m => m.DocumentId == document.Id && m.Type == 6);
            if (signActivity != null)
            {
                db.Activities.Remove(signActivity);
                db.SaveChanges();
            }
        }

        public static void SetSignTask(ncelsEntities db, Document document, Guid parentId)
        {
            Activity activity = new Activity
            {
                Id = Guid.NewGuid(),
                //	ParentTask = task.Id,
                DocumentId = document.Id,
                AuthorId = UserHelper.GetCurrentEmployee().Id.ToString(),
                AuthorValue = UserHelper.GetCurrentEmployee().DisplayName,
                ExecutorsId = document.RegistratorId,
                ExecutorsValue = document.RegistratorValue,
                //ResponsibleId = DictionaryHelper.GetItemsId(taskAction.ResponsibleId),
                //ResponsibleValue = DictionaryHelper.GetItemsName(taskAction.ResponsibleId),
                Type = 6,
                IsParrent = false,
                CreatedDate = DateTime.Now,
                ParentId = parentId,
                ExecutionDate = document.ExecutionDate,
                Text = "Подписать",
                IsNotActive = true,
                //IsMainLine = task.IsMainLine
            };
            db.Activities.Add(activity);
            db.SaveChanges();
        }

        public static void SetSignTaskAsParent(ncelsEntities db, Document document)
        {
            Activity activity = new Activity
            {
                Id = Guid.NewGuid(),
                //	ParentTask = task.Id,
                DocumentId = document.Id,
                AuthorId = UserHelper.GetCurrentEmployee().Id.ToString(),
                AuthorValue = UserHelper.GetCurrentEmployee().DisplayName,
                ExecutorsId = document.RegistratorId,
                ExecutorsValue = document.RegistratorValue,
                //ResponsibleId = DictionaryHelper.GetItemsId(taskAction.ResponsibleId),
                //ResponsibleValue = DictionaryHelper.GetItemsName(taskAction.ResponsibleId),
                Type = 6,
                IsParrent = true,
                CreatedDate = DateTime.Now,
                ParentId = null,
                ExecutionDate = document.ExecutionDate,
                Text = "Подписать",
                //IsMainLine = task.IsMainLine
            };
            db.Activities.Add(activity);
            db.SaveChanges();
        }

        public static bool IsEditRegisterProjects(ncelsEntities db,Guid id)
        {
            var doc = db.Documents.FirstOrDefault(m => m.Id == id);
            if (doc == null)
            {
                return true;
            }
            return doc.IsTradeSecret;
        }

        public static string GetProxyOrganizationName(ncelsEntities db, Guid id){
            try {
                var pp = db.PriceProjects.FirstOrDefault(p => p.Id == id);
                Document doc;
                if (pp == null)
                {
                    doc = db.Documents.FirstOrDefault(d => d.Id == id);
                    if (doc == null || string.IsNullOrEmpty(doc.AnswersId))
                        return null;
                    pp = db.PriceProjects.FirstOrDefault(p => p.Id == new Guid(doc.AnswersId));
                    if (pp == null)
                        return null;
                }
                var org = db.Organizations.FirstOrDefault(o => o.Id == pp.ProxyOrganizationId);
                if (org == null)
                    return null;
                return org.NameRu;
            }
            catch (Exception) {
                return null;
            }

        }
    }

}
