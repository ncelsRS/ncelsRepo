using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PW.Ncels.Database.DataModel;
using PW.Ncels.Database.Helpers;
using PW.Ncels.Database.Models;
using PW.Prism.Controllers.Base;

namespace PW.Prism.Controllers.Lims
{
    public class LimsAgreementController : LimsBaseController
    {
        #region Agreement 

        public ActionResult RequestAgreementList()
        {
            Guid guid = Guid.NewGuid();
            return PartialView(guid);
        }
        /*
        public ActionResult SendAgreement(Guid id, string url)
        {
            return PartialView(
                new SendAgreementViewModel()
                {
                    Id = id,
                    Url = url
                });
        }

        [HttpPost]
        public ActionResult ConfirmAgreement(Guid id)
        {
            var tmcin = db.TmcIns.First(m => m.Id == id);
            if (tmcin.StateType == 12)
            {
                tmcin.StateType = 1;
            }
            else if (new[] { 10, 11 }.Contains(tmcin.StateType))
            {
                tmcin.StateType = tmcin.StateType + 1;
            }
            db.SaveChanges();
            return Json(id, JsonRequestBehavior.AllowGet);
        }

        public ActionResult RejectAgreement(Guid id, int type, string url)
        {
            var createEmployeeId = UserHelper.GetCurrentEmployee().Id;
            //            var applicationComment = db.TmcApplicationComments
            //                .Where(a => a.ApplicationId == id)
            //                .OrderByDescending(a => a.CreateDate)
            //                .FirstOrDefault();
            var applicationComment = new TmcApplicationComment()
            {
                Id = Guid.NewGuid(),
                ApplicationId = id,
                CreateEmployeeId = createEmployeeId,
                Type = type,
                Url = url
            };

            return PartialView(applicationComment);
        }

        [HttpPost]
        public ActionResult ConfirmRejectAgreement(TmcApplicationComment comment)
        {
            var commentApp = db.TmcApplicationComments.FirstOrDefault(m => m.Id == comment.Id);

            if (commentApp == null)
            {
                commentApp = comment;
                db.TmcApplicationComments.Add(commentApp);
            }
            else
            {
                commentApp.Comment = comment.Comment;
            }


            if (comment.Type == 1)
            {
                var tmcIn = db.TmcIns.First(m => m.Id == comment.ApplicationId);
                tmcIn.StateType = -1;
            }
            else if (comment.Type == 2)
            {
                var tmcOut = db.TmcOuts.First(m => m.Id == comment.ApplicationId);
                tmcOut.StateType = -1;
            }

            db.SaveChanges();
            return Json(comment.Id, JsonRequestBehavior.AllowGet);
        }
        */
        #endregion
    }
}