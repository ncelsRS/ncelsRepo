using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using Aspose.Pdf;
using Kendo.Mvc.Infrastructure.Implementation;
using PW.Ncels.Database.DataModel;
using PW.Ncels.Database.Helpers;
using PW.Ncels.Database.Models;
using PW.Ncels.Database;
using Document = PW.Ncels.Database.DataModel.Document;
using SaveFormat = Aspose.Words.SaveFormat;

namespace PW.Ncels.Database.Helpers
{
	public class DocumentManager {
		public const string Root = "Attachments";
		public const string RootPreview = "Preview";
		public static string PathRoot = ConfigurationManager.AppSettings["AttachPath"];// @"C:\Documents";
		public const string Extension = @"preview.pdf";
		public const string Separator = ", ";

		private static string GetTextQRcode(string singInfo, bool isSing) {
			string text = "Подписано: " + "{0}" + "\n" +
						 "Дата подписи: " + "{1}" + " \n" +
						 "ЭЦП: " + "{2}";
			return string.Format(text, singInfo, DateTime.Now.ToString("dd.MM.yyyy hh:mm"), isSing ? "Да" : "Нет");
		}

		private static void DeleteOldDocument(ncelsEntities objectContext, Document project) {
			if (!string.IsNullOrEmpty(project.DestinationId)) {
				Guid id = new Guid(project.DestinationId);
				Document document = objectContext.Documents.First(o => o.Id == id);
				document.IsDeleted = true;
				//objectContext.Documents.AttachAsModified(document);
			}
		}
		public static void ReplaceText(string objectId, string name, string from, string to) {
			string fileName = Path.Combine(PathRoot, Root, objectId, name);
			if (File.Exists(fileName)) {
			string filePdf = Path.Combine(PathRoot, Root, objectId, name + RootPreview, Extension);
			Aspose.Words.Document doc = new Aspose.Words.Document(fileName);
			doc.Range.Replace(from, to, false, true);
			doc.Save(fileName);
			doc.Save(filePdf, SaveFormat.Pdf); 
			}
		}

		public static void CopyFile(string fileId, string newFile) {
			string filePath = Path.Combine(PathRoot, Root, fileId);
			string newFilePath = Path.Combine(PathRoot, Root, newFile);
			CopyDirectory(filePath, newFilePath, true);
		}

		private static bool CopyDirectory(string sourcePath, string destinationPath, bool overwriteexisting) {
			bool ret = false;
			try {
				sourcePath = sourcePath.EndsWith(@"\") ? sourcePath : sourcePath + @"\";
				destinationPath = destinationPath.EndsWith(@"\") ? destinationPath : destinationPath + @"\";

				if (Directory.Exists(sourcePath)) {
					if (Directory.Exists(destinationPath) == false)
						Directory.CreateDirectory(destinationPath);

					foreach (string fls in Directory.GetFiles(sourcePath)) {
						FileInfo flinfo = new FileInfo(fls);
						flinfo.CopyTo(destinationPath + flinfo.Name, overwriteexisting);
					}
					foreach (string drs in Directory.GetDirectories(sourcePath)) {
						DirectoryInfo drinfo = new DirectoryInfo(drs);
						if (CopyDirectory(drs, destinationPath + drinfo.Name, overwriteexisting) == false)
							ret = false;
					}
				}
				ret = true;
			} catch (Exception ex) {
				ret = false;
			}
			return ret;
		}

		/// <summary>
		/// Конвертирование в исходящий 
		/// </summary>
		/// <param name="objectContext"></param>
		/// <param name="project"></param>
		/// <param name="currentEmployee"></param>
		/// <param name="singInfo">Информация о подписывающем</param>
		public static void ConvertInOutgouingDocument(Document document, Document project, Employee currentEmployee, string singInfo) {
			ncelsEntities context = UserHelper.GetCn();
			document.QrCode = GetQrCode(GetTextQRcode(singInfo, !string.IsNullOrEmpty(project.Digest)));
			document.MainTaskId = project.MainTaskId;
			document.MainDocumentId = project.MainDocumentId;
			document.SourceId = project.Id.ToString();
			document.SourceValue = project.DisplayName;
			document.RegistratorValue = currentEmployee.DisplayName;
			document.RegistratorId = currentEmployee.Id.ToString();
			document.DocumentType = 1;
			document.SortNumber = 99999;
			document.StateType = 0;
			document.TemplateId = null;
			document.Number = null;
			document.DocumentDate = document.CreatedDate = document.ModifiedDate = DateTime.Now;
			document.SortNumber = 0;
			document.SortingNumber = null;
			document.IsDeleted = false;
			document.IsAttachments = false;
            document.AttachPath = FileHelper.GetObjectPathRoot();
            if (document.MainDocumentId != null) {
                Document doc = context.Documents.FirstOrDefault(o => o.Id == document.MainDocumentId.Value);
                if (doc != null) {
                    document.OrganizationId = doc.OrganizationId;
                }
            }
            CopyFile(project.AttachPath.ToString(), document.AttachPath.ToString());
			context.Documents.Add(document);
			context.SaveChanges();
		}

		/// <summary>
		/// Конвертирование во внутренний документ 
		/// </summary>
		/// <param name="objectContext"></param>
		/// <param name="project"></param>
		/// <param name="currentEmployee"></param>
		/// <param name="singInfo">Информация о подписывающем</param>
		public static void ConvertInCorrespondentDocument(Document document, Document project, Employee currentEmployee,
			string singInfo)
		{
			ncelsEntities context = UserHelper.GetCn();
			Template template = context.Templates.First(o => o.Id == project.TemplateId);
			document.QrCode = GetQrCode(GetTextQRcode(singInfo, !string.IsNullOrEmpty(project.Digest)));
			//Template convTemplate = context.Templates.First(o => o.Id == template.ConvertDictionaryTypeId);
			//document.DocumentDictionaryTypeId = convTemplate.DictionaryTypeId;
			//document.DocumentDictionaryTypeValue = convTemplate.DictionaryTypeValue;
			document.MainTaskId = project.MainTaskId;
			document.MainDocumentId = project.MainDocumentId;
			document.SourceId = project.Id.ToString();
			document.SourceValue = project.DisplayName;
			document.RegistratorValue = currentEmployee.DisplayName;
			document.RegistratorId = currentEmployee.Id.ToString();
			document.DocumentType = 5;
			document.TemplateId = context.Templates.First().Id;

			document.IsDeleted = false;
			document.DocumentDate = document.CreatedDate = document.ModifiedDate = DateTime.Now;
			document.ExecutionDate = document.ExecutionDate.HasValue ? document.ExecutionDate : DateTime.Now.AddDays(15);
			document.IsAttachments = false;
            document.AttachPath = FileHelper.GetObjectPathRoot();

			if (document.ApplicantType == 0)
			{
              
                document.StateType = 2;
				Registrator.SetNumber(document);
				context.Activities.Add(GetNewActivity(document));
				CopyFile(project.AttachPath.ToString(), document.AttachPath.ToString());
				ReplaceText(document.AttachPath.ToString(), "Проект.docx", "DocumentNumber", document.Number);
				ReplaceText(document.AttachPath.ToString(), "Проект.docx", "DocumentDate",
					document.DocumentDate.Value.ToString("dd.MM.yyyy"));
			}
			else
			{
                var items = DictionaryHelper.GetItems(document.ExecutorsId, document.ExecutorsValue).Select(o=>new Guid(o.Id)).ToList();

                var employes = context.Employees.Where(o=> items.Contains(o.Id)).ToList();
                var orgId = new Guid("8F0B91F3-AF29-4D3C-96D6-019CBBDFC8BE");
                if (employes.Select(o => o.OrganizationId).Contains(orgId)) {
                    document.OrganizationId = orgId;
                }


                CopyFile(project.AttachPath.ToString(), document.AttachPath.ToString());
				document.StateType = 0;
				document.Number = null;

			}

		    project.DestinationId = document.Id.ToString();
		    project.DestinationValue = document.Number;

            context.Documents.Add(document);
			context.SaveChanges();
		}

		public static void ConvertInAdminDocument(Document document, Document project, Employee currentEmployee,
			string singInfo)
		{
			ncelsEntities context = UserHelper.GetCn();
			Template template = context.Templates.First(o => o.Id == project.TemplateId);
			document.QrCode = GetQrCode(GetTextQRcode(singInfo, !string.IsNullOrEmpty(project.Digest)));
			//Template convTemplate = context.Templates.First(o => o.Id == template.ConvertDictionaryTypeId);
			//document.DocumentDictionaryTypeId = convTemplate.DictionaryTypeId;
			//document.DocumentDictionaryTypeValue = convTemplate.DictionaryTypeValue;
			document.MainTaskId = project.MainTaskId;
			document.MainDocumentId = project.MainDocumentId;
			document.SourceId = project.Id.ToString();
			document.SourceValue = project.DisplayName;
			document.RegistratorValue = currentEmployee.DisplayName;
			document.RegistratorId = currentEmployee.Id.ToString();
			document.DocumentType = 3;
			document.TemplateId = context.Templates.First().Id;

			document.IsDeleted = false;
			document.DocumentDate = document.CreatedDate = document.ModifiedDate = DateTime.Now;
			document.ExecutionDate = document.ExecutionDate.HasValue ? document.ExecutionDate : DateTime.Now.AddDays(15);
			document.IsAttachments = false;
            document.AttachPath = FileHelper.GetObjectPathRoot();

            CopyFile(project.AttachPath.ToString(), document.AttachPath.ToString());

			if (document.ProjectType == 4) {
				document.StateType = 2;
				Registrator.SetNumber(document);
				context.Activities.Add(GetNewActivity(document));
				ReplaceText(document.AttachPath.ToString(), "Проект.docx", "DocumentNumber", document.Number);
				ReplaceText(document.AttachPath.ToString(), "Проект.docx", "DocumentDate", document.DocumentDate.Value.ToString("dd.MM.yyyy"));
			}
			if (document.ProjectType == 3) {
				document.Number = null;
				document.StateType = 0;
			}
			if (document.ProjectType == 6) {
				document.Number = null;
				document.StateType = 0;
			}
			context.Documents.Add(document);
			context.SaveChanges();
		}

		private static Activity GetNewActivity(Document document) {
			return new Activity {
				Id = Guid.NewGuid(),
				IsParrent = true,
				ExecutorsId = document.ExecutorsId,
				ExecutorsValue = document.ExecutorsValue,
				CreatedDate = DateTime.Now,
				ExecutionDate = document.ExecutionDate,
				DocumentId = document.Id,
				DocumentValue = document.DisplayName,
				Text = "Рассмотреть",
				ResponsibleId = string.IsNullOrEmpty(document.ExecutorsId) ? "" : document.ExecutorsId.Split(new[] { Separator }, StringSplitOptions.RemoveEmptyEntries).FirstOrDefault(),
				ResponsibleValue = string.IsNullOrEmpty(document.ExecutorsValue) ? "" : document.ExecutorsValue.Split(new[] { Separator }, StringSplitOptions.RemoveEmptyEntries).FirstOrDefault(),
				Type = 2,
				AuthorId = document.CreatedUserId,
				AuthorValue = document.CreatedUserValue,
				IsMainLine = true
			};
		}
		private static byte[] GetQrCode(string data) {
			//var model = new QRCodeModel();
			//model.Content = data;
			//var client = new QRCodeClient();
			//var stream = client.Generate(model);
			//byte[] bytes =  GetByte(stream);
			//stream.Close();
			return null;
		}

		public static void CreateIncomingDocs(Document document)
		{
			ncelsEntities context = UserHelper.GetCn();
			List<Item> items = DictionaryHelper.GetItems(document.CorrespondentsId, document.CorrespondentsValue);
			List<string> listEmail = new List<string>();
			if (items.Count > 0)
			{
				foreach (var item in items) {
					Unit unit = context.Units.Find(new Guid(item.Id));

					if (unit.Type == 0) {
						Document doc = context.Documents.FirstOrDefault(x => x.OrganizationId == unit.Id && x.SourceId == document.Id.ToString());
						if (doc == null) {
							Document inDoc = new Document() {
								Id = Guid.NewGuid(),
								Summary = document.Summary,
								OutgoingDate = document.DocumentDate,
								OutgoingNumber = document.Number,
								DocumentDate = DateTime.Now,
								MonitoringType = document.MonitoringType,
								CorrespondentsId = UserHelper.GetCurrentEmployee().OrganizationId.ToString(),
								CorrespondentsValue = UserHelper.GetCurrentEmployee().Organization.Name,
								CorrespondentsInfo = document.SignerValue,
								ApplicantName = document.CreatedUserValue,
								ApplicantEmail = UserHelper.GetCurrentEmployee().Organization.Email,
								ExecutionDate = document.ExecutionDate,
								DocumentKindDictionaryId = document.DocumentKindDictionaryId,
								DocumentKindDictionaryValue = document.DocumentKindDictionaryValue,
								QuestionDesignDictionaryId = document.QuestionDesignDictionaryId,
								QuestionDesignDictionaryValue = document.QuestionDesignDictionaryValue,
								Counters = document.Counters,
								PageCount = document.PageCount,
								CopiesCount = document.CopiesCount,
								LanguageDictionaryId = document.LanguageDictionaryId,
								LanguageDictionaryValue = document.LanguageDictionaryValue,
								OrganizationId = unit.Id,
								SourceId = document.Id.ToString(),
								SourceValue = document.DisplayName,
                                AttachPath = FileHelper.GetObjectPathRoot()
							};
							context.Documents.Add(inDoc);
							CopyFile(document.AttachPath.ToString(), inDoc.AttachPath.ToString());
					
						}
					}
					context.SaveChanges();
				}
			}
		}
		private static byte[] GetByte(Stream input) {
			byte[] buffer = new byte[16 * 1024];
			using (MemoryStream ms = new MemoryStream()) {
				int read;
				while ((read = input.Read(buffer, 0, buffer.Length)) > 0) {
					ms.Write(buffer, 0, read);
				}
				return ms.ToArray();
			}
		}

		public static Document Clone(Document source)
		{
			Document document = new Document();
			document.IsDeleted = source.IsDeleted;
			document.IsAdministrativeUse = source.IsAdministrativeUse;
			document.IsAwaitingResponse = source.IsAwaitingResponse;
			document.IsTradeSecret = source.IsTradeSecret;
			document.CreatedDate = source.CreatedDate;
			document.ModifiedDate = source.ModifiedDate;
			document.AwaitingResponseDate = source.AwaitingResponseDate;
			document.DocumentDate = source.DocumentDate;
			document.OutgoingDate = source.OutgoingDate;
			document.ProtocolDate = source.ProtocolDate;
			document.MonitoringDate = source.MonitoringDate;
			document.ApplicantType = source.ApplicantType;
			document.DocumentType = source.DocumentType;
			document.MonitoringType = source.MonitoringType;
			document.PriorityType = source.PriorityType;
			document.StateType = source.StateType;
			document.AppendixCount = source.AppendixCount;
			document.CopiesCount = source.CopiesCount;
			document.PageCount = source.PageCount;
			document.RepeatCount = source.RepeatCount;
			document.ApplicantAddress = source.ApplicantAddress;
			document.ApplicantEmail = source.ApplicantEmail;
			document.ApplicantName = source.ApplicantName;
			document.ApplicantPhone = source.ApplicantPhone;
			document.BlankNumber = source.BlankNumber;
			document.CorrespondentsInfo = source.CorrespondentsInfo;
			document.Number = source.Number;
			document.OutgoingNumber = source.OutgoingNumber;
			document.SortingNumber = source.SortingNumber;
			document.SortingOutgoingNumber = source.SortingOutgoingNumber;
			document.Note = source.Note;
			document.Summary = source.Summary;
			document.AdministrativeTypeDictionaryId = source.AdministrativeTypeDictionaryId;
			document.AdministrativeTypeDictionaryValue = source.AdministrativeTypeDictionaryValue;
			document.ApplicantCategoryDictionaryId = source.ApplicantCategoryDictionaryId;
			document.ApplicantCategoryDictionaryValue = source.ApplicantCategoryDictionaryValue;
			document.CauseCitizenDictionaryId = source.CauseCitizenDictionaryId;
			document.CauseCitizenDictionaryValue = source.CauseCitizenDictionaryValue;
			document.CitizenCategoryDictionaryId = source.CitizenCategoryDictionaryId;
			document.CitizenCategoryDictionaryValue = source.CitizenCategoryDictionaryValue;
			document.CitizenResultDictionaryId = source.CitizenResultDictionaryId;
			document.CitizenResultDictionaryValue = source.CitizenResultDictionaryValue;
			document.CitizenTypeDictionaryId = source.CitizenTypeDictionaryId;
			document.CitizenTypeDictionaryValue = source.CitizenTypeDictionaryValue;
			document.DocumentKindDictionaryId = source.DocumentKindDictionaryId;
			document.DocumentKindDictionaryValue = source.DocumentKindDictionaryValue;
			document.FormDeliveryDictionaryId = source.FormDeliveryDictionaryId;
			document.FormDeliveryDictionaryValue = source.FormDeliveryDictionaryValue;
			document.FormSendingDictionaryId = source.FormSendingDictionaryId;
			document.FormSendingDictionaryValue = source.FormSendingDictionaryValue;
			document.KatoDictionaryId = source.KatoDictionaryId;
			document.KatoDictionaryValue = source.KatoDictionaryValue;
			document.LanguageDictionaryId = source.LanguageDictionaryId;
			document.LanguageDictionaryValue = source.LanguageDictionaryValue;
			document.NomenclatureDictionaryId = source.NomenclatureDictionaryId;
			document.NomenclatureDictionaryValue = source.NomenclatureDictionaryValue;
			document.QuestionDesignDictionaryId = source.QuestionDesignDictionaryId;
			document.QuestionDesignDictionaryValue = source.QuestionDesignDictionaryValue;
			document.SigningFormDictionaryId = source.SigningFormDictionaryId;
			document.SigningFormDictionaryValue = source.SigningFormDictionaryValue;
			document.CompleteDocumentsId = source.CompleteDocumentsId;
			document.EditDocumentsId = source.EditDocumentsId;
			document.RepealDocumentsId = source.RepealDocumentsId;
			document.RepeaterId = source.RepeaterId;
			document.AttachmentId = source.AttachmentId;
			document.ResolutionId = source.ResolutionId;
			document.AgreementsId = source.AgreementsId;
			document.AgreementsValue = source.AgreementsValue;
			document.ExecutorsId = source.ExecutorsId;
			document.ExecutorsValue = source.ExecutorsValue;
			document.ReadersId = source.ReadersId;
			document.ReadersValue = source.ReadersValue;
			document.RecipientsId = source.RecipientsId;
			document.RecipientsValue = source.RecipientsValue;
			document.RegistratorId = source.RegistratorId;
			document.RegistratorValue = source.RegistratorValue;
			document.ResponsibleId = source.ResponsibleId;
			document.ResponsibleValue = source.ResponsibleValue;
			document.SignerId = source.SignerId;
			document.SignerValue = source.SignerValue;
			document.CorrespondentsId = source.CorrespondentsId;
			document.CorrespondentsValue = source.CorrespondentsValue;
			document.MonitoringAuthorId = source.MonitoringAuthorId;
			document.MonitoringAuthorValue = source.MonitoringAuthorValue;
			document.MonitoringNote = source.MonitoringNote;
			document.AnswersId = source.AnswersId;
			document.AnswersValue = source.AnswersValue;
			document.CompleteDocumentsValue = source.CompleteDocumentsValue;
			document.EditDocumentsValue = source.EditDocumentsValue;
			document.RepealDocumentsValue = source.RepealDocumentsValue;
			document.DisplayName = source.DisplayName;
			document.AutoAnswersId = source.AutoAnswersId;
			document.AutoAnswersValue = source.AutoAnswersValue;
			document.AutoAnswersTempId = source.AutoAnswersTempId;
			document.AutoAnswersTempValue = source.AutoAnswersTempValue;
			document.AutoCompleteDocumentsValue = source.AutoCompleteDocumentsValue;
			document.AutoEditDocumentsValue = source.AutoEditDocumentsValue;
			document.AutoRepealDocumentsValue = source.AutoRepealDocumentsValue;
			document.SortNumber = source.SortNumber;
			document.AutoCompleteDocumentsId = source.AutoCompleteDocumentsId;
			document.AutoEditDocumentsId = source.AutoEditDocumentsId;
			document.AutoRepealDocumentsId = source.AutoRepealDocumentsId;
			document.FactExecutionDate = source.FactExecutionDate;
			document.FirstExecutionDate = source.FirstExecutionDate;
			document.ExecutionDate = source.ExecutionDate;
			document.TemplateId = source.TemplateId;
			document.Counters = source.Counters;
			document.DocumentDictionaryTypeId = source.DocumentDictionaryTypeId;
			document.DocumentDictionaryTypeValue = source.DocumentDictionaryTypeId;
			document.ResolutionValue = source.ResolutionValue;
			document.OutgoingType = source.OutgoingType;
			document.SourceId = source.SourceId;
			document.SourceValue = source.SourceValue;
			document.DestinationId = source.DestinationId;
			document.DestinationValue = source.DestinationValue;
			document.MainTaskId = source.MainTaskId;
			document.MainDocumentId = source.MainDocumentId;
			document.OwnerId = source.OwnerId;
			document.OwnerValue = source.OwnerValue;
			document.Country = source.Country;
			document.Area = source.Area;
			document.Postcode = source.Postcode;
			document.Phone = source.Phone;
			document.Department = source.Department;
			document.City = source.City;
			document.Address = source.Address;
			document.NumberBill = source.NumberBill;
			document.Email = source.Email;
			document.SuperMainDocumentId = source.SuperMainDocumentId;
			document.ModifiedUser = source.ModifiedUser;
			document.IsNotification = source.IsNotification;
			document.NotificationCount = source.NotificationCount;
			document.DateDispatch = source.DateDispatch;
			document.DispatchNote = source.DispatchNote;
			document.Digest = source.Digest;
			document.IsAttachments = source.IsAttachments;
			document.Text = source.Text;
			document.Recipient = source.Recipient;
			document.QrCode = source.QrCode;
			document.IsArchive = source.IsArchive;
			document.InventoryId = source.InventoryId;
			document.FulfilledDate = source.FulfilledDate;
			document.Year = source.Year;
			document.Month = source.Month;
			document.Day = source.Day;
			document.AutoAwaitingResponseDate = source.AutoAwaitingResponseDate;
			document.AutoDocumentDate = source.AutoDocumentDate;
			document.AutoOutgoingDate = source.AutoOutgoingDate;
			document.AutoProtocolDate = source.AutoProtocolDate;
			document.AutoMonitoringDate = source.AutoMonitoringDate;
			document.AutoFactExecutionDate = source.AutoFactExecutionDate;
			document.AutoFirstExecutionDate = source.AutoFirstExecutionDate;
			document.AutoExecutionDate = source.AutoExecutionDate;
			document.Deed = source.Deed;
			document.Book = source.Book;
			document.Akt = source.Akt;
			document.CreatedUserId = source.CreatedUserId;
			document.CreatedUserValue = source.CreatedUserValue;
			document.ProjectType = source.ProjectType;
		    document.AttachPath = source.AttachPath;
			return document;
		}
	}
}