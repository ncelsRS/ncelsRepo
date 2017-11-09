using System;
using PW.Ncels.Database.DataModel;

namespace Ncels.Core.ActivityManager
{
    public abstract class DocumentAction
    {
        protected ncelsEntities _context;

        public DocumentAction(ncelsEntities context)
        {
            _context = context;
        }
        public abstract Guid GetExecutor(Guid documentId, Guid activityTypeId, int orderNum);

        /// <summary>
        /// Начало процесса
        /// </summary>
        /// <param name="documentId"></param>
        /// <param name="activity"></param>
        public abstract void ProcessStarted(Guid documentId, EXP_Activities activity);

        /// <summary>
        /// завершена задача
        /// </summary>
        /// <param name="documentId"></param>
        /// <param name="task"></param>
        public abstract void TaskExecuted(Guid documentId, EXP_Tasks task);

        /// <summary>
        /// отклонен
        /// </summary>
        /// <param name="documentId"></param>
        /// <param name="task"></param>
        public abstract void TaskRejected(Guid documentId, EXP_Tasks task);

        /// <summary>
        /// отозван
        /// </summary>
        /// <param name="documentId"></param>
        /// <param name="task"></param>
        public abstract void TaskRepealed(Guid documentId, EXP_Tasks task);

        /// <summary>
        /// на завершении процесса
        /// </summary>
        /// <param name="documentId"></param>
        /// <param name="activity"></param>
        public abstract void ProcessComplited(Guid documentId, EXP_Activities activity);
        public abstract string GetDataForSign(Guid documentId);
    }
}