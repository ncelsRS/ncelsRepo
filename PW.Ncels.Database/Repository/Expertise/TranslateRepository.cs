using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PW.Ncels.Database.Constants;
using PW.Ncels.Database.DataModel;
using PW.Ncels.Database.Helpers;
using PW.Ncels.Database.Repository.Common;

namespace PW.Ncels.Database.Repository.Expertise
{
    public class TranslateRepository : ADrugDeclarationRepository
    {
        public FileLink SendToApplicant(string id, Employee getCurrentEmployee)
        {
            var file = AppContext.FileLinks.FirstOrDefault(e => e.Id == new Guid(id));
            if (file == null)
            {
                return null;
            }
            var status = new ReadOnlyDictionaryRepository().GetDicFileLinkStatusByCode(CodeConstManager.STATUS_FILE_CODE_SENDED);
            file.StatusId = status.Id;
            AppContext.SaveChanges();

            var declaraion = AppContext.EXP_DrugDeclaration.FirstOrDefault(e => e.Id == file.DocumentId);
            if (declaraion != null)
            {
                declaraion.DesignDate = DateTime.Now;
                declaraion.StatusId = CodeConstManager.STATUS_EXP_SEND_INSTRUCTION_ID;
                new DrugDeclarationRepository().Update(declaraion);
                var history = new EXP_DrugDeclarationHistory()
                {
                    DateCreate = DateTime.Now,
                    DrugDeclarationId = declaraion.Id,
                    StatusId = declaraion.StatusId,
                    UserId = getCurrentEmployee.Id,
                    Note = "Инструкция для согласования"
                };
                new DrugDeclarationRepository().SaveHisotry(history, UserHelper.GetCurrentEmployee().Id);
            }
            file.DIC_FileLinkStatus = status;
            return file;
        }


    }
}
