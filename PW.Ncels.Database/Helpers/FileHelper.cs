using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Linq.Dynamic;
using System.Text;
using System.Web;
using Aspose.Cells;
using Aspose.Pdf;
using Aspose.Slides;
using Aspose.Words;
using Ncels.Helpers;
using PW.Ncels.Database.Constants;
using PW.Ncels.Database.DataModel;
using PW.Ncels.Database.Models;
using PW.Ncels.Database.Repository.OBK;
using Cell = Aspose.Cells.Cell;
using Document = Aspose.Words.Document;
using PixelFormat = System.Drawing.Imaging.PixelFormat;
using Rectangle = System.Drawing.Rectangle;
using Image = System.Drawing.Image;
using LoadFormat = Aspose.Words.LoadFormat;
using LoadOptions = Aspose.Words.LoadOptions;
using SaveFormat = Aspose.Words.SaveFormat;

namespace PW.Ncels.Database.Helpers
{
    public static class FileHelper
    {
        public static string GetObjectPathRoot()
        {
            return $@"{DateTime.Now.Year}\\{DateTime.Now.Month}\\{Guid.NewGuid()}\\";
        }
        public static byte[] ReadFully(Stream input)
        {
            byte[] buffer = new byte[16 * 1024];
            using (MemoryStream ms = new MemoryStream())
            {
                int read;
                while ((read = input.Read(buffer, 0, buffer.Length)) > 0)
                {
                    ms.Write(buffer, 0, read);
                }
                return ms.ToArray();
            }
        }

        public static void ReplaceText(string objectId, string name, string from, string to)
        {
            string fileName = Path.Combine(PathRoot, Root, objectId, name);
            string filePdf = Path.Combine(PathRoot, Root, objectId, name + RootPreview, Extension);
            try
            {
                Document doc = new Document(fileName);
                doc.Range.Replace(from, to, false, true);

                doc.Save(fileName);
                doc.Save(filePdf, SaveFormat.Pdf);
            }
            catch (Exception) { }
        }


        public static void ReplaceText(MemoryStream memoryStream, string objectId, string name, IEnumerable<ReplaceItem> items)
        {

            string fileName = Path.Combine(PathRoot, Root, objectId, name);
            string filePdf = Path.Combine(PathRoot, Root, objectId, name + RootPreview, Extension);
            Document doc = new Document(memoryStream);

            foreach (var replaceItem in items)
            {
                doc.Range.Replace(replaceItem.Key, string.IsNullOrEmpty(replaceItem.Value) ? string.Empty : replaceItem.Value.Replace("\n", "").Replace("\r", ""), false, true);
            }


            doc.Save(fileName);
            doc.Save(filePdf, SaveFormat.Pdf);
        }
        public static void ReplaceTextExcel(string objectId, string name, IEnumerable<ReplaceItem> items)
        {

            string fileName = Path.Combine(PathRoot, Root, objectId, name);
            string filePdf = Path.Combine(PathRoot, Root, objectId, name + RootPreview, Extension);
            Workbook workbook = new Workbook(fileName);


            Worksheet worksheet = workbook.Worksheets[0];

            FindOptions opts = new FindOptions();


            Cell cell = null;
            foreach (var replaceItem in items)
            {
                do
                {
                    cell = worksheet.Cells.Find(replaceItem.Key, cell, opts);

                    if (cell != null)
                    {
                        cell.Value = replaceItem.Value;


                    }

                } while (cell != null);
            }
            //find each cell containing hello and replace it with
            //blue color hello world in arial black font


            workbook.Save(fileName);
            workbook.Save(filePdf, Aspose.Cells.SaveFormat.Pdf);
        }

        public static byte[] GetPreviewImage(string id, string name, bool? isArhive, string page = "0")
        {
            string previewFileName = Path.Combine(GetRoot(isArhive), Root, id, name + RootPreview, Extension);
            LogHelper.Log.DebugFormat("preview file = {0}", previewFileName);
            FileInfo info = new FileInfo(previewFileName);
            if (info.Exists)
            {
                using (Stream file = info.Open(FileMode.Open))
                {
                    return GetByte(file);
                }
            }
            DirectoryInfo dirInfo = new DirectoryInfo(Path.Combine(GetRoot(isArhive), Root, id, name + RootPreview));
            if (!dirInfo.Exists)
            {
                dirInfo.Create();
            }
            return GeneratePrewiew(id, name, previewFileName, isArhive);
        }

        private static byte[] GeneratePrewiew(string id, string name, string previewFileName, bool? isArchive)
        {
            try {
                FileInfo info;
                string savefileName = Path.Combine(GetRoot(isArchive), Root, id, name);
                LogHelper.Log.DebugFormat("GeneratePrewiew savefileName = {0}", savefileName);
                Stream stream = File.Open(savefileName, FileMode.Open);
                MemoryStream memoryStream = new MemoryStream(GetByte(stream)) {
                    Position = 0
                };
                LogHelper.Log.DebugFormat("1. memoryStream.Length = {0}", memoryStream.Length);
                Convert2Pdf(memoryStream, name, previewFileName);
                memoryStream.Close();
                memoryStream.Dispose();

                stream.Close();
                stream.Dispose();
                info = new FileInfo(previewFileName);
                using (Stream file = info.Open(FileMode.Open)) {
                    return GetByte(file);
                }
            }
            catch (Exception ex) {
                LogHelper.Log.Error("GeneratePrewiew exception", ex);
            }
            return null;
        }

        public static string GetFileName(string id, string name)
        {
            return Path.Combine(PathRoot, Root, id, name);
        }
        private static byte[] GetByte(Stream input)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                input.CopyTo(ms);
                return ms.ToArray();
            }
        }
        public const string Line1 = "ҚАЗАҚСТАН РЕСПУБЛИКАСЫ ДЕНСАУЛЫҚ САҚТАУ";
        public const string Line2 = "ЖӘНЕ ӘЛЕУМЕТТІК ДАМУ МИНИСТРЛІГІНІҢ";
        public const string Line3 = "\"РЕСПУБЛИКАЛЫҚ ЭЛЕКТРОНДЫҚ ДЕНСАУЛЫҚ САҚТАУ ОРТАЛЫҒЫ\"";
        public const string Line4 = "ШАРУАШЫЛЫҚ ЖҰРҒІЗУ ҚҰҚЫҒЫНДАҒЫ";
        public const string Line5 = "РЕСПУБЛИКАЛЫҚ МЕМЛЕКЕТТІК  КӘСШОРНЫ";
        public const string Line6 = "Кіріс №";
        public const string Line7 = "{0}ж.";
        public const string Line8 = "бөт";
        public const string Line9 = "қосымша";

        public const string Root = "Attachments";
        private const string RootPreview = "Preview";
        public static readonly string PathRoot = System.Configuration.ConfigurationManager.AppSettings["AttachPath"];
        private const string Extension = @"preview.pdf";

        public static void GenerationStamp(string fileid, string fileName)
        {
            string savefileName = Path.Combine(PathRoot, Root, fileid, Path.GetFileName(fileName));
            string filePdf = Path.Combine(PathRoot, Root, fileid, fileName + RootPreview, Extension);
            ncelsEntities entities = UserHelper.GetCn();
            PW.Ncels.Database.DataModel.Document document = entities.Documents.Find(new Guid(fileid));
            Guid guid = Guid.NewGuid();
            string pathImage = Path.Combine(PathRoot, guid + ".bmp");
            document.DocumentDate = document.DocumentDate ?? DateTime.Now;
            using (var rectangleFont = new System.Drawing.Font("PostScript", 24))
            using (var rectangleFont2 = new System.Drawing.Font("PostScript", 48))
            using (var bitmap = new Bitmap(1140, 336, PixelFormat.Format24bppRgb))
            using (var g = Graphics.FromImage(bitmap))
            {

                System.Drawing.Color color = System.Drawing.Color.FromArgb(68, 145, 235);
                System.Drawing.Color color1 = System.Drawing.Color.FromArgb(57, 49, 125);
                g.SmoothingMode = SmoothingMode.HighQuality;
                var backgroundColor = System.Drawing.Color.White;
                g.Clear(backgroundColor);
                g.DrawString(Line1, rectangleFont, new SolidBrush(color), new PointF(140, 1));
                g.DrawString(Line2, rectangleFont, new SolidBrush(color), new PointF(200, 35));
                g.DrawString(Line3, rectangleFont, new SolidBrush(color), new PointF(4, 70));
                g.DrawString(Line4, rectangleFont, new SolidBrush(color), new PointF(225, 105));
                g.DrawString(Line5, rectangleFont, new SolidBrush(color), new PointF(180, 140));

                g.DrawString(Line6, rectangleFont, new SolidBrush(color), new PointF(20, 200));
                g.DrawString(document.Number, rectangleFont2, new SolidBrush(color1), new PointF(150, 170));

                g.DrawString(string.Format(Line7, document.DocumentDate.Value.Year), rectangleFont, new SolidBrush(color),
                    new PointF(500, 200));
                g.DrawString(document.DocumentDate.Value.ToString("dd.MM"), rectangleFont2, new SolidBrush(color1),
                    new PointF(640, 170));

                g.DrawString(Line8, rectangleFont, new SolidBrush(color), new PointF(20, 280));
                g.DrawString(document.PageCount.ToString(), rectangleFont2, new SolidBrush(color1), new PointF(150, 250));

                g.DrawString(Line9, rectangleFont, new SolidBrush(color), new PointF(450, 280));
                if (string.IsNullOrEmpty(document.Counters))
                    g.DrawString(document.CopiesCount.ToString(), rectangleFont2, new SolidBrush(color1), new PointF(640, 250));
                else
                    g.DrawString(document.CopiesCount.ToString() + ", " + document.Counters, rectangleFont2, new SolidBrush(color1), new PointF(640, 250));
                g.DrawLine(new Pen(color, 2), 145, 230, 500, 230);
                g.DrawLine(new Pen(color, 2), 610, 230, 970, 230);

                g.DrawLine(new Pen(color, 2), 80, 310, 450, 310);
                g.DrawLine(new Pen(color, 2), 600, 310, 970, 310);

                g.DrawRectangle(new Pen(color, 2), new Rectangle(0, 0, 1138, 334));
                bitmap.Save(pathImage, ImageFormat.Bmp);
            }


            if (Path.GetExtension(fileName).ToLower() == ".pdf")
            {

                using (Aspose.Pdf.Document pdfDocument = new Aspose.Pdf.Document(savefileName))
                {

                    ImageStamp stamp = new ImageStamp(pathImage);

                    stamp.XIndent = 350;
                    stamp.YIndent = 40;
                    stamp.Zoom = 0.17;
                    pdfDocument.Pages[1].AddStamp(stamp);
                    pdfDocument.Save(savefileName);


                }
            }
            if (Path.GetExtension(fileName).ToLower() == ".jpeg" ||
                Path.GetExtension(fileName).ToLower() == ".jpg" ||
                Path.GetExtension(fileName).ToLower() == ".bmp" ||
                Path.GetExtension(fileName).ToLower() == ".png")
            {
                //using (MemoryStream stream = new MemoryStream())
                //{


                using (var image = Image.FromFile(pathImage))
                {
                    byte[] bytes = null;
                    using (FileStream fileStream = File.OpenRead(savefileName))
                    {
                        bytes = ReadFully(fileStream);
                    }
                    MemoryStream stream = new MemoryStream(bytes);
                    using (var img = Image.FromStream(stream))
                    {
                        Graphics g1 = Graphics.FromImage(img);
                        int height = Convert.ToInt32(img.Height * 0.068 * 3.4);
                        int width = Convert.ToInt32(img.Height * 0.068);
                        g1.DrawImage(image, new Rectangle(img.Width - height - 50 * img.Height / 1316, img.Height - width - 50 * img.Height / 1316, height, width));

                        if (Path.GetExtension(fileName).ToLower() == ".jpeg" || Path.GetExtension(fileName).ToLower() == ".jpg")
                        {

                            img.Save(savefileName, ImageFormat.Jpeg);
                        }

                        if (Path.GetExtension(fileName).ToLower() == ".bmp")
                        {
                            img.Save(savefileName, ImageFormat.Bmp);
                        }

                        if (Path.GetExtension(fileName).ToLower() == ".png")
                        {
                            img.Save(savefileName, ImageFormat.Png);
                        }
                    }
                    stream.Close();

                }

                //FileStream fileStream = File.OpenWrite(savefileName);
                //stream.WriteTo(fileStream);
                //fileStream.Close();
                //	}
            }
            using (Aspose.Pdf.Document pdfDocument = new Aspose.Pdf.Document(filePdf))
            {
                ImageStamp stamp = new ImageStamp(pathImage);
                stamp.XIndent = 350;
                stamp.YIndent = 40;
                stamp.Zoom = 0.17;
                pdfDocument.Pages[1].AddStamp(stamp);
                pdfDocument.Save(filePdf);
            }

            //File.Delete(pathImage);
        }

        public static string GetRoot(bool? isArhive)
        {
            if (!isArhive.HasValue || isArhive.Value == false)
            {
                return PathRoot;
            }
            {
                ncelsEntities entities = UserHelper.GetCn();

                var archive = entities.Archives.FirstOrDefault(o => o.IsCurrent);
                if (archive != null)
                    return archive.Path;
            }
            return PathRoot;
        }

        /// <summary>
        /// Сохраняем файл
        /// </summary>
        /// <param name="stream"></param>
        /// <param name="fileid"></param>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static int BuildPreview(byte[] stream, string fileid, string fileName)
        {
            try
            {
                string filePath = Path.Combine(PathRoot, Root, fileid, Path.GetFileName(fileName) + RootPreview);
                DirectoryInfo directoryInfo = new DirectoryInfo(filePath);
                if (!directoryInfo.Exists)
                    directoryInfo.Create();

                string savefileName = Path.Combine(PathRoot, Root, fileid, Path.GetFileName(fileName));
                string previewFileName = Path.Combine(PathRoot, Root, fileid, Path.GetFileName(fileName) + RootPreview, Extension);

                if (File.Exists(savefileName))
                {
                    fileName = GetNotDuplicateFileName(fileid, fileName);
                    savefileName = Path.Combine(PathRoot, Root, fileid, Path.GetFileName(fileName));
                    previewFileName = Path.Combine(PathRoot, Root, fileid, Path.GetFileName(fileName) + RootPreview, Extension);
                }
                MemoryStream memoryStream = new MemoryStream(stream) { };
                memoryStream.Position = 0;

                int pageCount = Convert2Pdf(memoryStream, Path.GetFileName(fileName), previewFileName);

                using (FileStream fileStream = new FileStream(savefileName, FileMode.CreateNew))
                    fileStream.Write(stream, 0, stream.Length);

                return pageCount;

            }
            catch (Exception)
            {
                return 0;
            }
        }

        public static string GetNotDuplicateFileName(string fileid, string fileName)
        {
            string name = Path.GetFileNameWithoutExtension(fileName);
            string extension = Path.GetExtension(fileName);
            string newNameFile;
            string savefileName;
            int i = 0;
            do
            {
                i++;
                newNameFile = name + "(" + i + ")" + extension;
                savefileName = Path.Combine(PathRoot, Root, fileid, Path.GetFileName(newNameFile));
            }
            while (File.Exists(savefileName));
            return newNameFile;
        }
        /// <summary>
        /// Удаляем файл
        /// </summary>
        /// <param name="fileid"></param>
        /// <param name="fileName"></param>
        public static void DeleteFile(string fileid, string fileName)
        {
            string savefileName = Path.Combine(PathRoot, Root, fileid, fileName);
            string previewFileName = Path.Combine(PathRoot, Root, fileid, fileName + RootPreview);
            DirectoryInfo directoryInfo = new DirectoryInfo(previewFileName);
            if (directoryInfo.Exists)
                directoryInfo.Delete(true);

            FileInfo info = new FileInfo(savefileName);
            if (info.Exists)
                info.Delete();
        }

        /// <summary>
        /// Удаляем файл
        /// </summary>
        /// <param name="fileid"></param>
        /// <param name="fileName"></param>
        public static void DeletePreview(string fileid, string fileName)
        {
            string previewFileName = Path.Combine(PathRoot, Root, fileid, fileName + RootPreview);
            DirectoryInfo directoryInfo = new DirectoryInfo(previewFileName);
            if (directoryInfo.Exists)
                directoryInfo.Delete(true);
        }

        /// <summary>
        /// Информация о файлах
        /// </summary>
        /// <param name="fileid"></param>
        public static List<UploadInitialFile> GetFiles(string fileid, bool? isArhive)
        {
            string path = Path.Combine(GetRoot(isArhive), Root, fileid);
            DirectoryInfo directoryInfo = new DirectoryInfo(path);
            if (!directoryInfo.Exists)
                return new List<UploadInitialFile>();
            return
                directoryInfo.GetFiles()
                    .OrderBy(x => x.Name)
                    .Select(file => new UploadInitialFile(Path.GetFileName(file.Name), file.Length, file.Extension)
                    {
                        documentId = fileid
                    })
                    .ToList();
        }
        public static string GetFilePath(string objectId, string categoryId, string name, string fileId)
        {
            return Path.Combine(ConfigurationManager.AppSettings["AttachPath"], Root, objectId ?? "", categoryId ?? "", name ?? fileId ?? "");
        }
        public static string DeleteAttach(string id, string path, string name)
        {
            if (id != null)
            {
                var data = Path.Combine(ConfigurationManager.AppSettings["AttachPath"], Root, id, path, name);
                File.Delete(data);
            }
            return "ОК";
        }
        public static Guid DeleteAttach(ncelsEntities db, string categ, string doc, string fileId)
        {
            var fileLinkId = Guid.Parse(Path.GetFileNameWithoutExtension(fileId));
            var fileLink = db.FileLinks.FirstOrDefault(e => e.Id == fileLinkId);
            if (fileLink.ParentId != null)
            {
                var originFileId = fileLink.ParentId.Value;
                db.FileLinks.Remove(fileLink);
                db.SaveChanges();
                var data = Path.Combine(ConfigurationManager.AppSettings["AttachPath"], Root, doc, categ, fileId);
                File.Delete(data);
                return originFileId;
            }
            else
            {
                var prevFileLink =
                    db.FileLinks.Where(e => e.ParentId == fileLink.Id)
                        .OrderByDescending(e => e.Version)
                        .FirstOrDefault();
                if (prevFileLink == null)
                {
                    db.FileLinks.Remove(fileLink);
                    db.SaveChanges();
                    var data = Path.Combine(ConfigurationManager.AppSettings["AttachPath"], Root, doc, categ, fileId);
                    File.Delete(data);
                    return fileLink.Id;
                }
                else
                {
                    fileLink.CreateDate = prevFileLink.CreateDate;
                    fileLink.Version = prevFileLink.Version;
                    fileLink.FileName = prevFileLink.FileName;
                    db.FileLinks.Remove(prevFileLink);
                    db.SaveChanges();
                    File.Delete(Path.Combine(ConfigurationManager.AppSettings["AttachPath"], Root, doc, categ, fileId));
                    File.Move(Path.Combine(ConfigurationManager.AppSettings["AttachPath"], Root, doc, categ, string.Format("{0}{1}", prevFileLink.Id, Path.GetExtension(prevFileLink.FileName))),
                        Path.Combine(ConfigurationManager.AppSettings["AttachPath"], Root, doc, categ, fileId));
                }
            }
            return Guid.Empty;
        }
        public static object GetAttachList(ncelsEntities db, string doc, string type, bool byMetadata = false, string excludeCodes=null)
        {
            try
            {
                DirectoryInfo info = new DirectoryInfo(Path.Combine(ConfigurationManager.AppSettings["AttachPath"], Root, doc ?? ""));

                if (!info.Exists)
                    info.Create();
                var listFoleder = info.GetDirectories().Select(o => new Guid(o.Name));
                var exludeItems = !string.IsNullOrEmpty(excludeCodes) ? excludeCodes.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries) : null;
                
                var dicListQuery = exludeItems != null && exludeItems.Length > 0 ? db.Dictionaries.Where(o => o.Type == type && !exludeItems.Contains(o.Code)) :
                    db.Dictionaries.Where(o => o.Type == type);
                var dicList = dicListQuery.Where(o => listFoleder.Contains(o.Id)).Select(o => new { o.Id, o.Name, o.Code })
                    .OrderBy(e => e.Code).ThenBy(e => e.Name).ToList();

                if (byMetadata)
                {
                    var categoryCodes = dicList.Select(e => e.Code).ToList();
                    var docId = Guid.Parse(doc);
                    var fileMetadatas =
                        db.FileLinks.Where(e => e.DocumentId == docId && categoryCodes.Contains(e.FileCategory.Code))
                            .ToList();
                    return dicList.Select(o => new
                    {
                        o.Id,
                        o.Name,
                        o.Code,
                        Items = (new DirectoryInfo(Path.Combine(ConfigurationManager.AppSettings["AttachPath"], Root, doc ?? "", o.Id.ToString()))).Exists ?
                    new DirectoryInfo(Path.Combine(ConfigurationManager.AppSettings["AttachPath"], Root, doc ?? "", o.Id.ToString())).GetFiles()
                    .Join(fileMetadatas, f => f.Name, f => string.Format("{0}{1}", f.Id, Path.GetExtension(f.FileName)), (f, fm) => new { File = f, FileMetadata = fm })
                    .ToList().Select(k => new
                    {
                        AttachId = string.Format("id={0}&path={1}&fileId={2}", o.Id, doc, string.Format("{0}{1}", k.FileMetadata.Id.ToString(), Path.GetExtension(k.FileMetadata.FileName))),
                        AttachName = k.FileMetadata.FileName,
                        sysCreatedDate = k.File.CreationTime,
                        AttachSize = k.File.Length,
                        k.FileMetadata.Version
                    }).ToList().Cast<object>()
                    : new List<object>()
                    });
                }

                var result = dicList.Select(o => new
                {
                    o.Id,
                    o.Name,
                    o.Code,
                    Items = new DirectoryInfo(Path.Combine(ConfigurationManager.AppSettings["AttachPath"], Root, doc ?? "", o.Id.ToString())).Exists
                    ? new DirectoryInfo(Path.Combine(ConfigurationManager.AppSettings["AttachPath"], Root, doc ?? "", o.Id.ToString())).GetFiles().ToList().Select(k => new
                    {
                        AttachId = string.Format("id={0}&path={1}&name={2}", o.Id, doc, k.Name),
                        AttachName = k.Name,
                        sysCreatedDate = k.CreationTime,
                        AttachSize = k.Length,
                        Version = 1
                    }).ToList().Cast<object>()
                    : new List<object>()
                });

                return result;
            }
            catch (Exception ex)
            {
                return new { IsError = true, Message = ex.Message };
            }
        }

        public static byte[] GetDocumentAttachFile(ncelsEntities db, Guid documentId) {
            try {
                var document = db.Documents.FirstOrDefault(x => x.Id == documentId);
                if (document == null)
                    return null;

                var dirPath = Path.Combine(PathRoot, Root, document.AttachPath);

                var dirInfo = new DirectoryInfo(dirPath);

                if (!dirInfo.Exists)
                    return null;

                var files = dirInfo.GetFiles();
                if (files.Length == 0)
                    return null;

                var file = files[0];
                using (FileStream stream = file.OpenRead()){
                    return GetByte(stream);
                }

            }
            catch (Exception ex){
                return null;
            }
        }

        public static IEnumerable GetAttachListByDoc(ncelsEntities db, string id, string dicid)
        {
            try
            {
                DirectoryInfo info = new DirectoryInfo(Path.Combine(ConfigurationManager.AppSettings["AttachPath"], id));

                if (!info.Exists)
                    info.Create();


                var dir =
                    new DirectoryInfo(Path.Combine(ConfigurationManager.AppSettings["AttachPath"], Root, id, dicid.ToString()));

                if (!dir.Exists)
                    dir.Create();
                var result = dir.GetFiles().ToList().Select(k => new { AttachId = string.Format("{0}\\\\{1}", id, dicid), AttachName = k.Name, sysCreatedDate = k.CreationTime, AttachSize = k.Length });
                //list.GroupBy(o => new { o.Id, o.Name })
                //    .Select(o => new { o.Key.Id, o.Key.Name, Items = o.Select(k => new { k.AttachId, k.sysCreatedDate, k.AttachName, k.AttachSize }) });


                return result;
            }
            catch (Exception ex)
            {
                return new List<object>();
            }
        }
        public static IEnumerable GetAttachListEdit(ncelsEntities db, string doc, string type, bool byMetadata = false, string excludeCodes = null, bool isShowComment=false)
        {
           /* try
            {*/
                DirectoryInfo info = new DirectoryInfo(Path.Combine(ConfigurationManager.AppSettings["AttachPath"], Root, doc ?? ""));
                if (!info.Exists)
                    info.Create();
                var exludeItems = !string.IsNullOrEmpty(excludeCodes) ? excludeCodes.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries) : null;
                var dicListQuery = exludeItems != null
                    ? db.Dictionaries.Where(o => o.Type == type && !exludeItems.Contains(o.Code))
                    : db.Dictionaries.Where(o => o.Type == type);
            if (byMetadata)
                {
                    var docId = Guid.Parse(doc);
                    var markList = db.FileLinksCategoryComs.Where(e => e.DocumentId == docId).ToList();
                        var dicListMeta = dicListQuery.Select(o => new { o.Id, o.Name, o.Code, ShowComment = isShowComment}).OrderBy(e => e.Code).ThenBy(x => x.Name).ToList();
                    var categoryCodes = dicListMeta.Select(e => e.Code).ToList();
                    var fileMetadatas =
                        db.FileLinks.Where(e => e.DocumentId == docId && categoryCodes.Contains(e.FileCategory.Code))
                            .ToList();
                    return dicListMeta.Select(o =>
                    {
                        var fileLinksCategoryCom = markList.FirstOrDefault(e => e.CategoryId == o.Id);
                        return new
                        {
                            o.Id,
                            o.Name,
                            o.Code,
                            o.ShowComment,
                            MarkClassName =
                            markList.FirstOrDefault(e => e.CategoryId == o.Id) == null
                                ? ""
                                : fileLinksCategoryCom != null && fileLinksCategoryCom.IsError
                                    ? "control-error"
                                    : "control-good",
                            Items =
                            (new DirectoryInfo(Path.Combine(ConfigurationManager.AppSettings["AttachPath"], Root,
                                doc ?? "", o.Id.ToString()))).Exists
                                ? new DirectoryInfo(Path.Combine(ConfigurationManager.AppSettings["AttachPath"], Root,
                                        doc ?? "", o.Id.ToString())).GetFiles()
                                    .Join(fileMetadatas, f => f.Name,
                                        f => string.Format("{0}{1}", f.Id, Path.GetExtension(f.FileName)),
                                        (f, fm) => new {File = f, FileMetadata = fm})
                                    .ToList().Select(k => new
                                    {
                                        AttachId =
                                        string.Format("id={0}&path={1}&fileId={2}", o.Id, doc,
                                            string.Format("{0}{1}", k.FileMetadata.Id,
                                                Path.GetExtension(k.FileMetadata.FileName))),
                                        AttachName = k.FileMetadata.FileName,
                                        sysCreatedDate = k.File.CreationTime,
                                        AttachSize = k.File.Length,
                                        k.FileMetadata.Version,
                                        OriginFileId = k.FileMetadata.ParentId,
                                        k.FileMetadata.OwnerName,
                                        CreateDate = k.FileMetadata.CreateDate.ToString(CultureInfo.InvariantCulture),
                                        MetadataId = k.FileMetadata.Id,
                                        k.FileMetadata.IsSigned,
                                        StatusCode =  k.FileMetadata.DIC_FileLinkStatus != null ? k.FileMetadata.DIC_FileLinkStatus.Code : "",
                                        StatusName =  k.FileMetadata.DIC_FileLinkStatus != null ? k.FileMetadata.DIC_FileLinkStatus.NameRu : "",
                                       
                                    }).ToList().Cast<object>()
                                : new List<object>()
                        };
                    });
                }
                var dicList = dicListQuery.Select(o => new { o.Id, o.Name, o.Code }).OrderBy(e => e.Code).ThenBy(x => x.Name).ToList();

                return dicList.Select(o => new
                {
                    o.Id,
                    o.Name,
                    o.Code,
                    Items = (new DirectoryInfo(Path.Combine(ConfigurationManager.AppSettings["AttachPath"], Root, doc ?? "", o.Id.ToString()))).Exists ?
                    new DirectoryInfo(Path.Combine(ConfigurationManager.AppSettings["AttachPath"], Root, doc ?? "", o.Id.ToString())).GetFiles().ToList().Select(k => new
                    {
                        AttachId = string.Format("id={0}&path={1}&name={2}", o.Id, doc, k.Name),
                        AttachName = k.Name,
                        sysCreatedDate = k.CreationTime,
                        AttachSize = k.Length,
                        Version = 1,
                        OriginFileId = (string)null,
                        MetadataId = (string)null
                    }).ToList().Cast<object>()
                    : new List<object>()
                });
  /*          }
            catch (Exception ex)
            {
                return new List<object>();
            }*/
        }
     
        public static bool CheckAttachList(ncelsEntities db, string id, string type)
        {
            try
            {
                DirectoryInfo info = new DirectoryInfo(Path.Combine(ConfigurationManager.AppSettings["AttachPath"], Root, id));
                if (!info.Exists)
                    info.Create();
                var listFoleder = info.GetDirectories().Select(o => new Guid(o.Name));

                var dicList = db.Dictionaries.Where(o => o.Type == type).Select(o => new { o.Id, o.Code, o.Name }).OrderBy(x => x.Name).ToList();

                var result = dicList.Select(o => new { o.Id, o.Code, o.Name, Items = (new DirectoryInfo(Path.Combine(ConfigurationManager.AppSettings["AttachPath"], Root, id, o.Id.ToString()))).Exists ? new DirectoryInfo(Path.Combine(ConfigurationManager.AppSettings["AttachPath"], Root, id, o.Id.ToString())).GetFiles().ToList().Select(k => new { AttachId = string.Format("{0}&path={1}&name={2}", o.Id, id, k.Name), AttachName = k.Name, sysCreatedDate = k.CreationTime, AttachSize = k.Length }).ToList().Cast<object>() : new List<object>() });
                //list.GroupBy(o => new { o.Id, o.Name })
                //    .Select(o => new { o.Key.Id, o.Key.Name, Items = o.Select(k => new { k.AttachId, k.sysCreatedDate, k.AttachName, k.AttachSize }) });
                int count = result.Where(item => item.Code == "dover" || item.Code == "letter").Count(item => item.Items.Any());

                return count == 2;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public static object SaveAttach(string code, string path, HttpRequestBase requestBase, bool saveMetadata, string originFileId, ncelsEntities db, string lang = "", string comment = "", int? numOfPages = null)
        {
            if (!saveMetadata && string.IsNullOrEmpty(code) && string.IsNullOrEmpty(path))
                throw new ArgumentException("Невозможно сохранить файл без привязки к объекту, без категории и без метаданных");
            if (saveMetadata && ((string.IsNullOrEmpty(code) && !string.IsNullOrEmpty(path)) || (!string.IsNullOrEmpty(code) && string.IsNullOrEmpty(path))))
                throw new ArgumentException("Невозможно сохранить файл с метаданными и привязкой к объекту или категории");

            if (requestBase.Files != null)
            {
                for (int i = 0; i < requestBase.Files.Count; i++)
                {
                    HttpPostedFileBase file = requestBase.Files[i];
                    Guid fileId = Guid.NewGuid();
                    string actualFileName = file.FileName;
                    string savedFileName = saveMetadata
                        ? string.Format("{0}{1}", fileId, Path.GetExtension(actualFileName))
                        : actualFileName;
                    try
                    {
                        var ownId = UserHelper.GetCurrentEmployee().Id;
                        int? currentStageId = null;
                        var pathGuid = Guid.Parse(path);
                        var stage =
                            db.EXP_ExpertiseStage.Where((x => x.Executors.Select(y => y.Id).Contains(ownId) && !x.IsHistory && x.EXP_DIC_StageStatus.Code == "inWork" && x.DeclarationId == pathGuid));
                        if (stage.Any())
                        {
                            currentStageId = stage.First().StageId;
                        }
                        string root = ConfigurationManager.AppSettings["AttachPath"];
                        string fullName = Path.Combine(root, Root, path ?? "", code ?? "", savedFileName);
                        DirectoryInfo info = new DirectoryInfo(Path.Combine(root, Root, path ?? "", code ?? ""));
                        if (!info.Exists)
                            info.Create();
                        file.SaveAs(fullName);
                        if (saveMetadata)
                        {
                            var fileLink = new FileLink()
                            {
                                Id = fileId,
                                CreateDate = DateTime.Now,
                                CategoryId = !string.IsNullOrEmpty(code) ? (Guid?)Guid.Parse(code) : null,
                                DocumentId = !string.IsNullOrEmpty(path) ? (Guid?)Guid.Parse(path) : null,
                                FileName = actualFileName,
                                OwnerId = ownId,
                                Version = 1,
                                Comment = comment,
                                Language = lang,
                                PageNumbers = numOfPages,
                                StageId = currentStageId

                            };
                            db.FileLinks.Add(fileLink);
                            db.SaveChanges();
                        }
                        if (saveMetadata && (string.IsNullOrEmpty(code) || string.IsNullOrEmpty(path)))
                            return new
                            {
                                Id = string.Format("fileId={0}", string.Format("{0}{1}", fileId, Path.GetExtension(savedFileName))),
                                MetadataId = fileId,
                                Version = 1
                            };
                        if (saveMetadata && !string.IsNullOrEmpty(code) && !string.IsNullOrEmpty(path))
                            return new
                            {
                                Id = string.Format("id={0}&path={1}&fileId={2}", code, path, string.Format("{0}{1}", fileId, Path.GetExtension(savedFileName))),
                                MetadataId = fileId,
                                Version = 1
                            };
                        return new
                        {
                            Id = string.Format("id={0}&path={1}&name={2}", code, path, actualFileName),
                            MetadataId = (string)null,
                            Version = (string)null
                        };
                    }
                    catch (Exception ex)
                    {
                        return ex.Message;
                    }
                }

            }
            return "Ок";
        }
        public static object SaveAttachNewVersion(string code, string path, string originFile, HttpRequestBase requestBase, ncelsEntities db)
        {
            if (!string.IsNullOrEmpty(originFile)
                && ((string.IsNullOrEmpty(code) && !string.IsNullOrEmpty(path)) || (!string.IsNullOrEmpty(code) && string.IsNullOrEmpty(path))))
                throw new ArgumentException("Невозможно сохранить файл с привязкой к объекту или категории");

            if (requestBase.Files != null)
            {
                for (int i = 0; i < requestBase.Files.Count; i++)
                {
                    HttpPostedFileBase file = requestBase.Files[i];
                    Guid historyId = Guid.NewGuid();
                    var originFileId = Guid.Parse(originFile);
                    var originFileLink = db.FileLinks.FirstOrDefault(e => e.Id == originFileId);
                    string historyFileName = string.Format("{0}{1}", historyId, Path.GetExtension(originFileLink.FileName));
                    string originFileName = string.Format("{0}{1}", originFileLink.Id, Path.GetExtension(originFileLink.FileName));
                    try
                    {
                        string root = ConfigurationManager.AppSettings["AttachPath"];
                        File.Move(Path.Combine(root, Root, path ?? "", code ?? "", originFileName ?? ""),
                            Path.Combine(root, Root, path ?? "", code ?? "", historyFileName ?? ""));
                        file.SaveAs(Path.Combine(root, Root, path ?? "", code ?? "", originFileName ?? ""));
                        var historyFileLink = new FileLink()
                        {
                            Id = historyId,
                            CreateDate = DateTime.Now,
                            CategoryId = originFileLink.CategoryId,
                            DocumentId = originFileLink.DocumentId,
                            FileName = originFileLink.FileName,
                            Version = originFileLink.Version,
                            OwnerId = UserHelper.GetCurrentEmployee().Id,
                            ParentId = originFileLink.Id
                        };
                        originFileLink.FileName = file.FileName;
                        originFileLink.CreateDate = DateTime.Now;
                        originFileLink.Version++;
                        db.FileLinks.Add(historyFileLink);
                        db.SaveChanges();

                        if ((string.IsNullOrEmpty(code) && string.IsNullOrEmpty(path)))
                            return new
                            {
                                Id = string.Format("fileId={0}", string.Format("{0}{1}", originFileLink.Id, Path.GetExtension(originFileLink.FileName))),
                                MetadataId = originFileLink.Id,
                                originFileLink.Version,
                                HistoryId = historyFileLink.Id,
                                HistoryPath = string.Format("fileId={0}", string.Format("{0}{1}", historyFileLink.Id, Path.GetExtension(historyFileLink.FileName)))
                            };
                        return new
                        {
                            Id = string.Format("id={0}&path={1}&fileId={2}", code, path, string.Format("{0}{1}", originFileLink.Id, Path.GetExtension(originFileLink.FileName))),
                            MetadataId = originFileLink.Id,
                            originFileLink.Version,
                            HistoryId = historyFileLink.Id,
                            HistoryPath = string.Format("id={0}&path={1}&fileId={2}", code, path, string.Format("{0}{1}", historyFileLink.Id, Path.GetExtension(historyFileLink.FileName)))
                        };
                    }
                    catch (Exception ex)
                    {
                        return ex.Message;
                    }
                }

            }
            return "Ок";
        }
        public static void SaveAttachNewVersion(string category, string document, string originFile, string fileName, Stream file, ncelsEntities db)
        {
            if (!string.IsNullOrEmpty(originFile)
                && ((string.IsNullOrEmpty(category) && !string.IsNullOrEmpty(document)) || (!string.IsNullOrEmpty(category) && string.IsNullOrEmpty(document))))
                throw new ArgumentException("Невозможно сохранить файл с привязкой к объекту или категории");

            Guid historyId = Guid.NewGuid();
            var originFileId = Guid.Parse(originFile);
            var originFileLink = db.FileLinks.FirstOrDefault(e => e.Id == originFileId);
            string historyFileName = string.Format("{0}{1}", historyId, Path.GetExtension(originFileLink.FileName));
            string originFileName = string.Format("{0}{1}", originFileLink.Id, Path.GetExtension(originFileLink.FileName));
            string root = ConfigurationManager.AppSettings["AttachPath"];
            File.Move(Path.Combine(root, Root, document ?? "", category ?? "", originFileName ?? ""),
                Path.Combine(root, Root, document ?? "", category ?? "", historyFileName ?? ""));
            var fileStream = File.Create(Path.Combine(root, Root, document ?? "", category ?? "", string.Format("{0}{1}", originFileLink.Id, Path.GetExtension(fileName))));
            file.CopyTo(fileStream);
            fileStream.Close();
            var historyFileLink = new FileLink()
            {
                Id = historyId,
                CreateDate = DateTime.Now,
                CategoryId = originFileLink.CategoryId,
                DocumentId = originFileLink.DocumentId,
                FileName = originFileLink.FileName,
                Version = originFileLink.Version,
                OwnerId = UserHelper.GetCurrentEmployee().Id,
                ParentId = originFileLink.Id
            };
            originFileLink.FileName = fileName;
            originFileLink.CreateDate = DateTime.Now;
            originFileLink.Version++;
            db.FileLinks.Add(historyFileLink);
            db.SaveChanges();
        }
        public static void SaveFile(string category, string document, string fileName, Stream file, ncelsEntities db)
        {
            if (((string.IsNullOrEmpty(category) && !string.IsNullOrEmpty(document)) || (!string.IsNullOrEmpty(category) && string.IsNullOrEmpty(document))))
                throw new ArgumentException("Невозможно сохранить файл с привязкой к объекту или категории");

            Guid fileLinkId = Guid.NewGuid();
            var fileLink = new FileLink()
            {
                Id = fileLinkId,
                DocumentId = Guid.Parse(document),
                CategoryId = Guid.Parse(category),
                CreateDate = DateTime.Now,
                FileName = fileName,
                Version = 1
            };
            string savedFileName = string.Format("{0}{1}", fileLink.Id, Path.GetExtension(fileLink.FileName));
            string root = ConfigurationManager.AppSettings["AttachPath"];
            var directory=new DirectoryInfo(Path.Combine(root, Root, document ?? "", category ?? ""));
            if(!directory.Exists)
                directory.Create();
            var fileStream = File.Create(Path.Combine(root, Root, document ?? "", category ?? "", savedFileName ?? ""));
            file.CopyTo(fileStream);
            fileStream.Close();
            db.FileLinks.Add(fileLink);
        }

        public static void UploadFile(string dir, string fileName, Stream file) {
            var dirPath = Path.Combine(PathRoot, Root, dir);
            var filePath = Path.Combine(dirPath, fileName);
            var directory=new DirectoryInfo(dirPath);
            if(!directory.Exists)
                directory.Create();
            var fileStream = File.Create(filePath);
            file.CopyTo(fileStream);
            fileStream.Close();
        }

        /// <summary>
        /// Скачиваем файл
        /// </summary>
        /// <param name="fileId"></param>
        /// <param name="fileName"></param>
        /// <param name="isArhive"></param>
        public static byte[] Download(string fileId, string fileName, bool? isArhive)
        {
            string filePath = Path.Combine(GetRoot(isArhive), Root, fileId, fileName);
            FileInfo fileInfo = new FileInfo(filePath);
            using (FileStream stream = fileInfo.OpenRead())
            {
                return GetByte(stream);
            }
        }

        private static int Convert2Pdf(MemoryStream memoryStream, string fileName, string previewFileName)
        {
            string extension = Path.GetExtension(fileName).ToLower();

            switch (extension)
            {
                case ".doc":
                    ConvertWord2Pdf(memoryStream, previewFileName, LoadFormat.Doc);
                    break;
                case ".docx":
                    ConvertWord2Pdf(memoryStream, previewFileName, LoadFormat.Docx);
                    break;
                case ".rtf":
                    ConvertWord2Pdf(memoryStream, previewFileName, LoadFormat.Rtf);
                    break;
                case ".htm":
                case ".html":
                    ConvertHtml2Pdf(memoryStream, previewFileName);
                    break;
                case ".xls":
                case ".xlsx":
                    ConvertExcell2Pdf(memoryStream, previewFileName);
                    break;
                case ".txt":
                    ConvertTxt2Pdf(memoryStream, previewFileName);
                    break;
                case ".pdf":
                    ConvertPdf2Pdf(memoryStream, previewFileName);
                    break;
                case ".gif":
                case ".jpg":
                case ".png":
                case ".jpeg":
                case ".bmp":
                    ConvertImage2Pdf(memoryStream, previewFileName);
                    break;
                case ".tif":
                case ".tiff":
                    ConvertTiff2Pdf(memoryStream, previewFileName);
                    break;
                case ".ppt":
                case ".pptx":
                    ConvertPpt2Pdf(memoryStream, previewFileName);
                    break;
            }
            return 0;
        }

        /// <summary>
        /// Word 2 Pdf
        /// </summary>
        /// <param name="memoryStream"></param>
        /// <param name="previewFileName"></param>
        /// <param name="format"></param>
        private static void ConvertWord2Pdf(Stream memoryStream, string previewFileName, LoadFormat format)
        {
            try
            {
                LoadOptions lopt = new LoadOptions { LoadFormat = format };
                Document document = new Document(memoryStream, lopt);

                //PdfSaveOptions pdfSaveOptions = new PdfSaveOptions {Compliance = PdfCompliance.Pdf15};

                document.Save(previewFileName, SaveFormat.Pdf);

                //Aspose.Pdf.Document pdf = new Aspose.Pdf.Document(previewFileName);

                //pdf.Pages[0].Resources.Images.Add(new FileStream("d:\\1.png", FileMode.Open));
                //pdf.Pages[0].Contents.Add(new Operator.q());

                //pdf.Save(previewFileName);

                //PdfFileMend mender = new PdfFileMend(previewFileName, "d:\\output.pdf");


                ////add image in the PDF file
                ////mender.AddImage("d:\\1.png", 1, 100, 600, 316, 682);
                //mender.AddImage("d:\\1.png", 1, 50, 50, 100, 100);

                ////create new FormattedText type object to add text in the PDF file
                //FormattedText ft = new FormattedText(
                //"PdfFileMend testing! 0 rotation.",
                //System.Drawing.Color.FromArgb(0, 200, 0),
                //FontStyle.TimesRoman,
                //EncodingType.Winansi,
                //false,
                //12);

                ////add text in the existing PDF file
                //mender.AddText(ft, 1, 50, 100, 100, 200);
                ////close PdfFileMend object
                //mender.Close();
            }
            catch (Exception exception)
            {
                var ex = exception;
            }
        }

        /// <summary>
        /// Excel 2 Pdf
        /// </summary>
        /// <param name="memoryStream"></param>
        /// <param name="previewFileName"></param>
        private static void ConvertExcell2Pdf(Stream memoryStream, string previewFileName)
        {
            try
            {
                Workbook document = new Workbook(memoryStream);
                //Aspose.Cells.PdfSaveOptions pdfSaveOptions = new Aspose.Cells.PdfSaveOptions{Compliance = Aspose.Cells.Rendering.PdfCompliance.PdfA1b};
                document.Save(previewFileName, Aspose.Cells.SaveFormat.Pdf);
            }
            catch (Exception) { }
        }

        /// <summary>
        /// Image 2 Pdf
        /// </summary>
        /// <param name="memoryStream"></param>
        /// <param name="previewFileName"></param>
        private static void ConvertImage2Pdf(MemoryStream memoryStream, string previewFileName)
        {
            try {
                Document document = new Document();
                DocumentBuilder builder = new DocumentBuilder(document);
                builder.PageSetup.LeftMargin = 0;
                builder.PageSetup.TopMargin = 0;
                builder.PageSetup.RightMargin = 0;
                builder.PageSetup.BottomMargin = 0;
                builder.InsertImage(ResizeImage(memoryStream, 1000));

                document.Save(previewFileName, SaveFormat.Pdf);
            }
            catch (Exception ex) {
                LogHelper.Log.Error("ConvertImage2Pdf Ошибка конвертации", ex);
            }
        }

        /// <summary>
        /// Ppt 2 Pdf
        /// </summary>
        /// <param name="memoryStream"></param>
        /// <param name="previewFileName"></param>
        private static void ConvertPpt2Pdf(Stream memoryStream, string previewFileName)
        {
            try
            {
                Presentation pres = new Presentation(memoryStream);
                pres.Save(previewFileName, Aspose.Slides.Export.SaveFormat.Pdf);
            }
            catch (Exception) { }
        }

        /// <summary>
        /// Html 2 Pdf
        /// </summary>
        /// <param name="memoryStream"></param>
        /// <param name="previewFileName"></param>
        private static void ConvertHtml2Pdf(Stream memoryStream, string previewFileName)
        {
            try
            {
                Document document = new Document();
                DocumentBuilder builder = new DocumentBuilder(document);
                using (StreamReader reader = new StreamReader(memoryStream, Encoding.Default))
                {
                    string line = reader.ReadToEnd();
                    if (!string.IsNullOrEmpty(line))
                        builder.InsertHtml(line);
                }
                document.Save(previewFileName);
            }
            catch (Exception) { }
        }

        /// <summary>
        /// Txt 2 Pdf
        /// </summary>
        /// <param name="memoryStream"></param>
        /// <param name="previewFileName"></param>
        private static void ConvertTxt2Pdf(Stream memoryStream, string previewFileName)
        {
            try
            {
                Document document = new Document();
                DocumentBuilder builder = new DocumentBuilder(document);
                using (StreamReader reader = new StreamReader(memoryStream, Encoding.Default))
                {
                    string line = reader.ReadToEnd();
                    if (!string.IsNullOrEmpty(line))
                        builder.Writeln(line);
                }
                document.Save(previewFileName);
            }
            catch (Exception) { }
        }

        /// <summary>
        /// Pdf 2 Pdf
        /// </summary>
        /// <param name="memoryStream"></param>
        /// <param name="previewFileName"></param>
        private static void ConvertPdf2Pdf(MemoryStream memoryStream, string previewFileName)
        {
            try
            {

                FileStream fileStream = File.Open(previewFileName, FileMode.OpenOrCreate);
                memoryStream.WriteTo(fileStream);
                fileStream.Flush();
                fileStream.Close();
                memoryStream.Flush();
                memoryStream.Close();
            }
            catch (Exception) { }
        }

        /// <summary>
        /// Tiff 2 Pdf
        /// </summary>
        /// <param name="memoryStream"></param>
        /// <param name="previewFileName"></param>
        private static void ConvertTiff2Pdf(Stream memoryStream, string previewFileName)
        {
            try
            {
                Document document = new Document();
                DocumentBuilder builder = new DocumentBuilder(document);
                Image imageTiff = Image.FromStream(memoryStream);
                int pageCount = imageTiff.GetFrameCount(FrameDimension.Page);
                for (int i = 0; i < pageCount; i++)
                {
                    imageTiff.SelectActiveFrame(FrameDimension.Page, i);
                    Image imagePng = new Bitmap(imageTiff);
                    imagePng = ResizeImage(imagePng, 500);
                    builder.InsertImage(imagePng, 500, 500);
                    if (i < pageCount - 1)
                    {
                        builder.MoveToDocumentEnd();
                        builder.InsertBreak(BreakType.PageBreak);
                    }
                }

                document.Save(previewFileName);
            }
            catch (Exception) { }
        }

        private static MemoryStream ResizeImage(MemoryStream memoryStream, int targetSize)
        {
            Image original = Image.FromStream(memoryStream);
            int targetH, targetW;
            if (original.Height > original.Width)
            {
                targetH = targetSize;
                targetW = (int)(original.Width * (targetSize / (float)original.Height));
            }
            else
            {
                targetW = targetSize;
                targetH = (int)(original.Height * (targetSize / (float)original.Width));
            }
            Image imgPhoto = Image.FromStream(memoryStream);
            // Create a new blank canvas.  The resized image will be drawn on this canvas.
            Bitmap bmPhoto = new Bitmap(targetW, targetH, PixelFormat.Format24bppRgb);
            Graphics grPhoto = Graphics.FromImage(bmPhoto);
            grPhoto.SmoothingMode = SmoothingMode.Default;
            grPhoto.InterpolationMode = InterpolationMode.Bilinear;
            grPhoto.PixelOffsetMode = PixelOffsetMode.HighSpeed;
            grPhoto.DrawImage(imgPhoto, new Rectangle(0, 0, targetW, targetH), 0, 0, original.Width, original.Height, GraphicsUnit.Pixel);
            // Save out to memory and then to a file.  We dispose of all objects to make sure the files don't stay locked.
            MemoryStream mm = new MemoryStream();
            bmPhoto.Save(mm, original.RawFormat);
            original.Dispose();
            imgPhoto.Dispose();
            bmPhoto.Dispose();
            grPhoto.Dispose();
            return mm;
        }

        private static Image ResizeImage(Image image, int targetSize)
        {
            Image original = image;
            int targetH, targetW;
            if (original.Height > original.Width)
            {
                targetH = targetSize;
                targetW = (int)(original.Width * (targetSize / (float)original.Height));
            }
            else
            {
                targetW = targetSize;
                targetH = (int)(original.Height * (targetSize / (float)original.Width));
            }
            Image imgPhoto = image;
            // Create a new blank canvas.  The resized image will be drawn on this canvas.
            Bitmap bmPhoto = new Bitmap(targetW, targetH, PixelFormat.Format24bppRgb);
            Graphics grPhoto = Graphics.FromImage(bmPhoto);
            grPhoto.SmoothingMode = SmoothingMode.Default;
            grPhoto.InterpolationMode = InterpolationMode.Bilinear;
            grPhoto.PixelOffsetMode = PixelOffsetMode.HighSpeed;
            grPhoto.DrawImage(imgPhoto, new Rectangle(0, 0, targetW, targetH), 0, 0, original.Width, original.Height, GraphicsUnit.Pixel);
            return imgPhoto;
        }

        public static MemoryStream ResizeImage2(MemoryStream memoryStream, int targetSize)
        {
            Image original = Image.FromStream(memoryStream);
            int targetH, targetW;
            if (original.Height > original.Width)
            {
                targetH = targetSize;
                targetW = (int)(original.Width * (targetSize / (float)original.Height)) + 65;
            }
            else
            {
                targetW = targetSize;
                targetH = (int)(original.Height * (targetSize / (float)original.Width));
            }
            Image imgPhoto = Image.FromStream(memoryStream);
            // Create a new blank canvas.  The resized image will be drawn on this canvas.
            Bitmap bmPhoto = new Bitmap(targetW, targetH, PixelFormat.Format24bppRgb);
            Graphics grPhoto = Graphics.FromImage(bmPhoto);
            grPhoto.SmoothingMode = SmoothingMode.Default;
            grPhoto.InterpolationMode = InterpolationMode.Bilinear;
            grPhoto.PixelOffsetMode = PixelOffsetMode.HighSpeed;
            grPhoto.DrawImage(imgPhoto,
                new Rectangle(0, 0, targetW, targetH),
                0,
                0,
                original.Width,
                original.Height,
                GraphicsUnit.Pixel);
            // Save out to memory and then to a file.  We dispose of all objects to make sure the files don't stay locked.
            MemoryStream mm = new MemoryStream();
            bmPhoto.Save(mm, original.RawFormat);
            original.Dispose();
            imgPhoto.Dispose();
            bmPhoto.Dispose();
            grPhoto.Dispose();
            return mm;
        }

        /// <summary>
        /// Кoпирование файла
        /// </summary>
        /// <param name="fileId"></param>
        /// <param name="newFile"></param>
        public static void CopyFile(string fileId, string newFile)
        {
            string filePath = Path.Combine(PathRoot, Root, fileId);
            string newFilePath = Path.Combine(PathRoot, Root, newFile);
            CopyDirectory(filePath, newFilePath, true);
        }

        private static bool CopyDirectory(string sourcePath, string destinationPath, bool overwriteexisting)
        {
            bool ret;
            try
            {
                sourcePath = sourcePath.EndsWith(@"\") ? sourcePath : sourcePath + @"\";
                destinationPath = destinationPath.EndsWith(@"\") ? destinationPath : destinationPath + @"\";

                if (Directory.Exists(sourcePath))
                {
                    if (Directory.Exists(destinationPath) == false)
                        Directory.CreateDirectory(destinationPath);

                    foreach (string fls in Directory.GetFiles(sourcePath))
                    {
                        FileInfo flinfo = new FileInfo(fls);
                        flinfo.CopyTo(destinationPath + flinfo.Name, overwriteexisting);
                    }
                    foreach (string drs in Directory.GetDirectories(sourcePath))
                    {
                        DirectoryInfo drinfo = new DirectoryInfo(drs);
                        if (CopyDirectory(drs, destinationPath + drinfo.Name, overwriteexisting) == false)
                            ret = false;
                    }
                }
                ret = true;
            }
            catch (Exception ex)
            {
                ret = false;
            }
            return ret;
        }

        public static void BuildPreview(byte[] stream, string fileid, string fileName, IEnumerable<ReplaceItem> items)
        {
            string extension = Path.GetExtension(fileName).ToLower();
            if (".docx" == extension)
            {
                string filePath = Path.Combine(PathRoot, Root, fileid, fileName + RootPreview);
                DirectoryInfo directoryInfo = new DirectoryInfo(filePath);
                if (!directoryInfo.Exists)
                    directoryInfo.Create();
                MemoryStream memoryStream = new MemoryStream(stream)
                {
                    Position = 0
                };
                ReplaceText(memoryStream, fileid, fileName, items);
            }
        }

        public static object SaveNextFileVersion(string code, string path, string originFile,
            HttpRequestBase requestBase, ncelsEntities db,string lang="",string comment="",int? numOfPages = null)
        {
            if (!string.IsNullOrEmpty(originFile)
                &&
                ((string.IsNullOrEmpty(code) && !string.IsNullOrEmpty(path)) ||
                 (!string.IsNullOrEmpty(code) && string.IsNullOrEmpty(path))))
                throw new ArgumentException("Невозможно сохранить файл с привязкой к объекту или категории");

            if (requestBase.Files != null)
            {
                for (int i = 0; i < requestBase.Files.Count; i++)
                {
                    HttpPostedFileBase file = requestBase.Files[i];
                    Guid newVersionId = Guid.NewGuid();
                    var originFileId = Guid.Parse(originFile);
                    var originFileLink = db.FileLinks.FirstOrDefault(e => e.Id == originFileId);
                    string newVersionFileName = string.Format("{0}{1}", newVersionId,
                        Path.GetExtension(file.FileName));
                    string originFileName = string.Format("{0}{1}", originFileLink.Id,
                        Path.GetExtension(originFileLink.FileName));
                    try
                    {
                        var ownId = UserHelper.GetCurrentEmployee().Id;
                        int? currentStageId = null;
                        var pathGuid = Guid.Parse(path);
                        var stage =
                            db.EXP_ExpertiseStage.Where((x => x.Executors.Select(y => y.Id).Contains(ownId) && !x.IsHistory && x.EXP_DIC_StageStatus.Code == "inWork" && x.DeclarationId == pathGuid));
                        if (stage.Any())
                        {
                            currentStageId = stage.First().StageId;
                        }
                        var oldFileNewName = originFileLink.FileName;
                        string root = ConfigurationManager.AppSettings["AttachPath"];
                        using (
                            var str =
                                File.OpenRead(Path.Combine(root, Root, path ?? "", code ?? "", originFileName ?? "")))
                        {
                            using (var mem = new MemoryStream())
                            {
                                str.CopyTo(mem);
                                var nameWithouExt = Path.GetFileNameWithoutExtension(originFileLink.FileName);
                                var oldFileSavingName = originFileLink.Id + ".pdf";
                                var newFullPath = Path.Combine(root, Root, path ?? "", code ?? "", oldFileSavingName ?? "");
                                Convert2Pdf(mem, originFileName, newFullPath);
                                oldFileNewName = nameWithouExt + ".pdf";
                            }
                        }
                            //File.Move(Path.Combine(root, Root, path ?? "", code ?? "", originFileName ?? ""),
                            //    Path.Combine(root, Root, path ?? "", code ?? "", historyFileName ?? ""));
                            file.SaveAs(Path.Combine(root, Root, path ?? "", code ?? "", newVersionFileName ?? ""));
                        var newVersionFileLink = new FileLink()
                        {
                            Id = newVersionId,
                            CreateDate = DateTime.Now,
                            CategoryId = originFileLink.CategoryId,
                            DocumentId = originFileLink.DocumentId,
                            FileName = file.FileName,
                            Version = originFileLink.Version + 1,
                            OwnerId = ownId,
                            ParentId = originFileLink.Id,
                            Language = lang,
                            Comment = comment,
                            PageNumbers = numOfPages,
                            StageId = currentStageId
                        };
                        db.FileLinks.Add(newVersionFileLink);
                        originFileLink.FileName = oldFileNewName;
                        db.SaveChanges();

                        //if ((string.IsNullOrEmpty(code) && string.IsNullOrEmpty(path)))
                        //    return new
                        //    {
                        //        Id =
                        //            string.Format("fileId={0}",
                        //                string.Format("{0}{1}", originFileLink.Id,
                        //                    Path.GetExtension(originFileLink.FileName))),
                        //        MetadataId = originFileLink.Id,
                        //        originFileLink.Version,
                        //        HistoryId = historyFileLink.Id,
                        //        HistoryPath =
                        //            string.Format("fileId={0}",
                        //                string.Format("{0}{1}", historyFileLink.Id,
                        //                    Path.GetExtension(historyFileLink.FileName)))
                        //    };
                        //return new
                        //{
                        //    Id =
                        //        string.Format("id={0}&path={1}&fileId={2}", code, path,
                        //            string.Format("{0}{1}", originFileLink.Id,
                        //                Path.GetExtension(originFileLink.FileName))),
                        //    MetadataId = originFileLink.Id,
                        //    originFileLink.Version,
                        //    HistoryId = historyFileLink.Id,
                        //    HistoryPath =
                        //        string.Format("id={0}&path={1}&fileId={2}", code, path,
                        //            string.Format("{0}{1}", historyFileLink.Id,
                        //                Path.GetExtension(historyFileLink.FileName)))
                        //};
                    }
                    catch (Exception ex)
                    {
                        return ex.Message;
                    }
                }

            }
            return "Ок";
        }
    }
}