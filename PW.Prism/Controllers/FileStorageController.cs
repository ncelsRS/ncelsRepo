using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PW.Ncels.Database.DataModel;
using PW.Ncels.Database.Helpers;
using PW.Prism.ViewModels;

namespace PW.Prism.Controllers
{
    [Authorize]
    public class FileStorageController : Controller
    {
        private ncelsEntities _context=new ncelsEntities();
        [HttpGet]   
        [AllowAnonymous]
        public FileResult Download(Guid id, FileType fileType)
        {
            var fileLink = GetFileLink(id, fileType);
            if (fileLink == null)
                throw new ArgumentException("Файл не найден");
            var fileName = FileHelper.GetFilePath(
                fileLink.DocumentId != null ? fileLink.DocumentId.Value.ToString() : null,
                fileLink.CategoryId != null ? fileLink.CategoryId.Value.ToString() : null,
                null, string.Format("{0}{1}", fileLink.Id, fileLink.Extension));
            return File(System.IO.File.ReadAllBytes(fileName),
                System.Net.Mime.MediaTypeNames.Application.Octet, fileLink.FileName);
        }

        private FileLink GetFileLink(Guid id, FileType fileType)
        {
            FileLink fileLink;
            switch (fileType)
            {
                case FileType.Contract:
                    if (_context.Contracts.Any(e => e.Id == id))
                    {
                        fileLink = _context.FileLinks.FirstOrDefault(e => e.DocumentId == id && e.ParentId == null);
                    }
                    else
                    {
                        fileLink = null;
                    }
                    break;
                default:
                    fileLink = null;
                    break;
            }
            return fileLink;
        }
    }
}