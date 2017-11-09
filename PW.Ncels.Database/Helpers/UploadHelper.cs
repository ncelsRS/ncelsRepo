using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using PW.Ncels.Database.DataModel;
using PW.Ncels.Database.Models;
using PW.Ncels.Database.DataModel;

namespace PW.Ncels.Database.Helpers
{
	public class UploadHelper
	{

		public static byte[] GetPreview(string id, string name, bool? isArhive) {

			return FileHelper.GetPreviewImage(id, name, isArhive);
		}


		public static byte[] Download(string id, string name, bool? isArhive) {
			return FileHelper.Download(id, name, isArhive);
		}

		public static void CppyFile(string fileId, string newFileId) {
			FileHelper.CopyFile(fileId, newFileId);
		}

		public static void ReplaceDocument(string objectId, string name, string from, string to) {
			FileHelper.ReplaceText(objectId, name, from, to);
		}
		public static void Upload(Stream fileInput, string name, string id)
		{
			var file = FileHelper.ReadFully(fileInput);
			
			FileHelper.BuildPreview(file, id.ToString(), name);

			ncelsEntities service = UserHelper.GetCn();
			Document document = service.Documents.FirstOrDefault(o => o.AttachPath == id);
			if (document != null && document.IsAttachments == false) {
				document.IsAttachments = true;
				service.SaveChanges();
			}
		}

		public static void GenerationStamp(string objectId, string name)
		{
			FileHelper.GenerationStamp(objectId,name);
		}
		public static void Upload(byte[] file, string name, Guid id) {
			FileHelper.BuildPreview(file, id.ToString(), name);

			ncelsEntities service = UserHelper.GetCn();
			Document document = service.Documents.FirstOrDefault(o => o.Id == id);
			if (document != null && document.IsAttachments == false) {
				document.IsAttachments = true;
				service.SaveChanges();
			}
		}

		public static void UploadReplace(byte[] file, string name, string id, List<ReplaceItem> items) {
			FileHelper.BuildPreview(file, id.ToString(), name, items);
			ncelsEntities service = UserHelper.GetCn();
			Document document = service.Documents.FirstOrDefault(o => o.AttachPath == id);
			if (document != null && document.IsAttachments == false) {
				document.IsAttachments = true;
				service.SaveChanges();
			}
		}
        


		public static byte[] GetFile(string name, Guid id, bool? isArhive, string page) {
			return FileHelper.GetPreviewImage(id.ToString(), name, isArhive, page);
		}

		public static List<UploadInitialFile> GetFilesInfo(string id, bool? isArhive) {
			return FileHelper.GetFiles(id, isArhive);
		}

		public static void DeleteFile(string id, string name) {
			FileHelper.DeleteFile(id, name);
			ncelsEntities service = UserHelper.GetCn();
			Document document = service.Documents.FirstOrDefault(o => o.AttachPath == id);
			if (document != null && document.IsAttachments) {
				document.IsAttachments = FileHelper.GetFiles(id, false).Count > 0;
				service.SaveChanges();
			}
		}

		public static void DeletePreview(string id, string name) {
			FileHelper.DeletePreview(id, name);
		} 
	}
}