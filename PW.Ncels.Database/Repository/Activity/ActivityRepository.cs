using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using PW.Ncels.Database.DataModel;

namespace PW.Ncels.Database.Repository.Activity
{
    public class ActivityRepository: ARepositoryGeneric<EXP_Activities>
    {
        public ActivityRepository(bool isProxy = true):base(isProxy)
        { }

        public ActivityRepository(ncelsEntities context) : base(context)
        {
            
        }

        public bool UpdateActivityStatus(Guid activityId, string statusCode)
        {
            var status = GetActivityStatusByCode(statusCode);
            var activity = AppContext.EXP_Activities.FirstOrDefault(e => e.Id == activityId);

            if (activity == null || status == null) return false;

            activity.StatusId = status.Id;
            // очень важно чтобы так заполнялось. не менять !!!!
            activity.StatusValue = status.Code;

            return true;
        }

        public Dictionary GetActivityStatusByCode(string code = null)
        {
            if (code == null)
                code = Dictionary.ExpActivityStatus.OnAgreement;
            var expActivityStatus = AppContext.Dictionaries
                .FirstOrDefault(e => e.Type == Dictionary.ExpActivityStatus.DicCode && e.Code == code);
            return expActivityStatus;
        }

        public Guid? GetActivityTypeIdByCode(string code)
        {
            Guid? id;
            if (string.IsNullOrEmpty(code))
                id = null;
            else
                id = AppContext.Dictionaries.Where(d => d.Type == Dictionary.ExpActivityType.DicCode && d.Code == code)
                    .Select(d => d.Id)
                    .First();

            return id;
        }
        public Dictionary GetActivityTypeById(Guid activityTypeId)
        {
            return AppContext.Dictionaries.FirstOrDefault(e => e.Id == activityTypeId);
        }

        public Guid? GetDocumentTypeIdByCode(string code)
        {
            Guid? id;
            if (string.IsNullOrEmpty(code))
                id = null;
            else
                id = AppContext.Dictionaries.Where(d => d.Type == Dictionary.ExpAgreedDocType.DicCode && d.Code == code)
                    .Select(d => d.Id)
                    .First();

            return id;
        }

        public Dictionary GetDocumentTypeById(Guid documentTypeId)
        {
            return AppContext.Dictionaries.FirstOrDefault(e=>e.Id==documentTypeId);
        }

        public Guid? GetTaskTypeIdByCode(string code)
        {
            Guid? id;
            if (string.IsNullOrEmpty(code))
                id = null;
            else
                id = AppContext.Dictionaries.Where(d => d.Type == Dictionary.ExpTaskType.DicCode && d.Code == code)
                    .Select(d => d.Id)
                    .First();

            return id;
        }

        public EXP_AgreementProcSettings GetSettings(Guid documentTypeId)
        {
            var setting = AppContext.EXP_AgreementProcSettings.FirstOrDefault(s => s.AgreedDocTypeId == documentTypeId);
            return setting;
        }
        
        
        public Dictionary GetTaskStatusByCode(string code)
        {
            var taskStatus = AppContext.Dictionaries
                .FirstOrDefault(e => e.Type == Dictionary.ExpTaskStatus.DicCode && e.Code == code);
            return taskStatus;
        }
        
        public IQueryable<EXP_Tasks> GetTasks(Expression<Func<EXP_Tasks, bool>> filter = null)
        {
            if (filter == null)
                return AppContext.EXP_Tasks;
            return AppContext.EXP_Tasks.Where(filter);
        }



        public IQueryable<EXP_AgreementProcSettingsTasks> GetSettingTasks(Expression<Func<EXP_AgreementProcSettingsTasks, bool>> filter = null)
        {
            if (filter == null)
                return AppContext.EXP_AgreementProcSettingsTasks;
            return AppContext.EXP_AgreementProcSettingsTasks.Where(filter);
        }

        public void AddSettingTask(EXP_AgreementProcSettingsTasks settingTask)
        {
            AppContext.EXP_AgreementProcSettingsTasks.Add(settingTask);
        }

        public void UpdateSettingTask(EXP_AgreementProcSettingsTasks settingTask)
        {
            AppContext.EXP_AgreementProcSettingsTasks.AddOrUpdate(settingTask);
        }

        public void DeleteSettingTask(Guid id)
        {
            var seettingTask = AppContext.EXP_AgreementProcSettingsTasks.FirstOrDefault(st => st.Id == id);
            if (seettingTask != null)
                AppContext.EXP_AgreementProcSettingsTasks.Remove(seettingTask);
        }




        public bool UpdateTaskStatus(Guid taskId, string statusCode)
        {
            return UpdateTaskStatus(taskId, statusCode, string.Empty, null);
        }

        public bool UpdateTaskStatus(Guid taskId, string statusCode, string comment, string digSign)
        {
            var status = GetTaskStatusByCode(statusCode);
            var task = AppContext.EXP_Tasks.FirstOrDefault(e => e.Id == taskId);

            if (task == null || status == null) return false;

            task.StatusId = status.Id;
            task.DigSign = digSign;
            // очень важно чтобы так заполнялось. не менять !!!!
            task.StatusValue = status.Code;
            task.Comment = comment;


            if (statusCode == Dictionary.ExpTaskStatus.Executed)
            {
                task.ExecutedDate = DateTime.Now;
            }
            return true;
        }

        public Employee GetExecutorById(Guid executorId)
        {
            return AppContext.Employees.FirstOrDefault(e => e.Id == executorId);
        }
    }
}