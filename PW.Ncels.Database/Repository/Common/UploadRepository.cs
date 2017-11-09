using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PW.Ncels.Database.Constants;
using PW.Ncels.Database.DataModel;
using PW.Ncels.Database.Models.Common;

namespace PW.Ncels.Database.Repository.Common
{
    public class UploadRepository :  ARepository
    {
        public const string Root = "Attachments";
        public FileLinksCategoryCom GetComments(string documentId, string categoryId)
        {
            return
                AppContext.FileLinksCategoryComs.FirstOrDefault(
                    e => e.DocumentId == new Guid(documentId) && e.CategoryId == new Guid(categoryId));
        }

        public void SaveComment(string documentId, string categoryId, bool isError, string comment, string userId)
        {

            var model = GetComments(documentId, categoryId);
            if (model == null)
            {
                model =new FileLinksCategoryCom
                {
                    Id = Guid.NewGuid(),
                    DocumentId = new Guid(documentId),
                    CategoryId = new Guid(categoryId),
                    DateCreate = DateTime.Now
                };
            }
            model.IsError = isError;
             var record =new FileLinksCategoryComRecord
            {
                CreateDate = DateTime.Now,
                Note = comment,
                FileLinksCategoryCom = model,
                UserId = new Guid(userId)
            };
            AppContext.FileLinksCategoryComRecords.Add(record);
            AppContext.SaveChanges();
        }
        public FileGroup GetAttachListbyCode(Guid? docId, string type, string code)
        {
            /* try
             {*/
            var doc = docId.ToString();
            DirectoryInfo info = new DirectoryInfo(Path.Combine(ConfigurationManager.AppSettings["AttachPath"], Root, doc ?? ""));
            if (!info.Exists)
                info.Create();
            var dicListQuery = AppContext.Dictionaries.Where(o => o.Type == type && o.Code == code);

            var markList = AppContext.FileLinksCategoryComs.Where(e => e.DocumentId == docId).ToList();
            var list = new List<FileGroup>();
            var dicListMeta = dicListQuery.Select(o => new { o.Id, o.Name, o.Code }).AsEnumerable().ToList();
            var categoryCodes = dicListMeta.Select(e => e.Code).ToList();
            var fileMetadatas =
                    AppContext.FileLinks.Where(e => e.DocumentId == docId && categoryCodes.Contains(e.FileCategory.Code))
                        .ToList();

            foreach (var dictionary in dicListQuery)
            {
                var fileLinksCategoryCom = markList.FirstOrDefault(e => e.CategoryId == dictionary.Id);
                var group = new FileGroup();
                group.Id = dictionary.Id;
                group.Code = dictionary.Code;
                group.Name = dictionary.Name;
                group.MarkClassName =
                    markList.FirstOrDefault(e => e.CategoryId == dictionary.Id) == null
                        ? "control-default"
                        : fileLinksCategoryCom != null && fileLinksCategoryCom.IsError
                            ? "control-error"
                            : "control-good";


                group.FileGroupItems =
                (new DirectoryInfo(Path.Combine(ConfigurationManager.AppSettings["AttachPath"], Root,
                    doc ?? "", dictionary.Id.ToString()))).Exists
                    ? new DirectoryInfo(Path.Combine(ConfigurationManager.AppSettings["AttachPath"], Root,
                            doc ?? "", dictionary.Id.ToString())).GetFiles()
                        .Join(fileMetadatas, f => f.Name,
                            f => string.Format("{0}{1}", f.Id, Path.GetExtension(f.FileName)),
                            (f, fm) => new { File = f, FileMetadata = fm })
                        .ToList().Select(k => new FileGroupItem()
                        {
                            AttachId =
                            string.Format("id={0}&path={1}&fileId={2}", dictionary.Id, doc,
                                string.Format("{0}{1}", k.FileMetadata.Id,
                                    Path.GetExtension(k.FileMetadata.FileName))),
                            AttachName = k.FileMetadata.FileName,
                            AttachSize = k.File.Length,
                            Version = k.FileMetadata.Version,
                            OriginFileId = k.FileMetadata.ParentId,
                            OwnerName = k.FileMetadata.OwnerName,
                            OwnerId = (Guid)k.FileMetadata.OwnerId,
                            CreateDate = k.FileMetadata.CreateDate.ToString(CultureInfo.InvariantCulture),
                            MetadataId = k.FileMetadata.Id,
                            Language = k.FileMetadata.Language,
                            Comment = k.FileMetadata.Comment,
                            DicFileLinkStatus = k.FileMetadata.DIC_FileLinkStatus,
                            Stage = k.FileMetadata.EXP_DIC_Stage != null ? k.FileMetadata.EXP_DIC_Stage.NameRu : ""
                        }).ToList()
                    : new List<FileGroupItem>();

                list.Add(group);
            }
            return list.FirstOrDefault();
        }

        public IEnumerable<FileGroup> GetAttachListEdit(Guid? docId, string type, string excludeCodes = null,bool needFillFilesStages = false)
        {
            /* try
             {*/
            var doc = docId.ToString();
            DirectoryInfo info = new DirectoryInfo(Path.Combine(ConfigurationManager.AppSettings["AttachPath"], Root, doc ?? ""));
            if (!info.Exists)
                info.Create();
            var exludeItems = !string.IsNullOrEmpty(excludeCodes) ? excludeCodes.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries) : null;
            var dicListQuery = exludeItems != null
                ? AppContext.Dictionaries.Where(o => o.Type == type && !exludeItems.Contains(o.Code))
                : AppContext.Dictionaries.Where(o => o.Type == type);
           
                var markList = AppContext.FileLinksCategoryComs.Where(e => e.DocumentId == docId).ToList();
            var list = new List<FileGroup>();
            var dicListMeta = dicListQuery.Select(o => new { o.Id, o.Name, o.Code }).AsEnumerable().ToList();
            var categoryCodes = dicListMeta.Select(e => e.Code).ToList();
            var fileMetadatas =
                    AppContext.FileLinks.Where(e => e.DocumentId == docId && categoryCodes.Contains(e.FileCategory.Code))
                        .ToList();
            foreach (var dictionary in dicListQuery)
            {
                var fileLinksCategoryCom = markList.FirstOrDefault(e => e.CategoryId == dictionary.Id);
                var group = new FileGroup();
                group.Id = dictionary.Id;
                group.Code = dictionary.Code;
                group.Name=dictionary.Name;
                group.MarkClassName =
                    markList.FirstOrDefault(e => e.CategoryId == dictionary.Id) == null
                        ? "control-default"
                        : fileLinksCategoryCom != null && fileLinksCategoryCom.IsError
                            ? "control-error"
                            : "control-good";


                group.FileGroupItems =
                (new DirectoryInfo(Path.Combine(ConfigurationManager.AppSettings["AttachPath"], Root,
                    doc ?? "", dictionary.Id.ToString()))).Exists
                    ? new DirectoryInfo(Path.Combine(ConfigurationManager.AppSettings["AttachPath"], Root,
                            doc ?? "", dictionary.Id.ToString())).GetFiles()
                        .Join(fileMetadatas, f => f.Name,
                            f => string.Format("{0}{1}", f.Id, Path.GetExtension(f.FileName)),
                            (f, fm) => new {File = f, FileMetadata = fm})
                        .ToList().Select(k => new FileGroupItem()
                        {
                            AttachId =
                            string.Format("id={0}&path={1}&fileId={2}", dictionary.Id, doc,
                                string.Format("{0}{1}", k.FileMetadata.Id,
                                    Path.GetExtension(k.FileMetadata.FileName))),
                            AttachName = k.FileMetadata.FileName,
                            AttachSize = k.File.Length,
                            Version =k.FileMetadata.Version,
                            OriginFileId = k.FileMetadata.ParentId,
                            OwnerName =k.FileMetadata.OwnerName,
                            OwnerId = (Guid)k.FileMetadata.OwnerId,
                            CreateDate = k.FileMetadata.CreateDate.ToString(CultureInfo.InvariantCulture),
                            MetadataId = k.FileMetadata.Id,
                            Comment = k.FileMetadata.Comment,
                            DicFileLinkStatus = k.FileMetadata.DIC_FileLinkStatus,
                            Language = k.FileMetadata.Language,
                            NumOfPages = k.FileMetadata.PageNumbers,
                            Stage = needFillFilesStages && k.FileMetadata.EXP_DIC_Stage != null ? k.FileMetadata.EXP_DIC_Stage.NameRu:""
                        }).ToList()
                    : new List<FileGroupItem>();
              
                list.Add(group);
            }
            return list;
        }

        public FileLink GetFileLinkById(Guid id)
        {
            return AppContext.FileLinks.FirstOrDefault(e => e.Id==id);
        }

        public FileLink AcceptFileConfirm(Guid id)
        {
            var model = AppContext.FileLinks.FirstOrDefault(e => e.Id == id);
            if (model == null)
            {
                return null;
            }
            var status =
                new ReadOnlyDictionaryRepository().GetDicFileLinkStatusByCode(CodeConstManager.STATUS_FILE_CODE_ACCEPTED);
            model.StatusId = status.Id;
            model.Comment = "";
             AppContext.SaveChanges();
            model.DIC_FileLinkStatus = status;
            return model;

        }

        public FileLink RejectFileConfirm(Guid id, string note)
        {
            var model = AppContext.FileLinks.FirstOrDefault(e => e.Id == id);
            if (model == null)
            {
                return null;
            }
            var status =
                new ReadOnlyDictionaryRepository().GetDicFileLinkStatusByCode(CodeConstManager.STATUS_FILE_CODE_FOR_REVISION);
            model.StatusId = status.Id;
            model.Comment = note;
            AppContext.SaveChanges();
            model.DIC_FileLinkStatus = status;
            return model;
        }
    }
}
