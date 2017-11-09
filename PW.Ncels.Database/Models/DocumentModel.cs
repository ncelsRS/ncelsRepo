using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using PW.Ncels.Database.DataModel;
using PW.Ncels.Database.Helpers;
using PW.Ncels.Database.Recources;


namespace PW.Ncels.Database.Models
{
	public class DocumentModel
	{
	
		public DocumentModel(Document document)
		{
			Id = document.Id;
			IsDeleted = document.IsDeleted;
			IsAdministrativeUse = document.IsAdministrativeUse;
			IsAwaitingResponse = document.IsAwaitingResponse;
			IsTradeSecret = document.IsTradeSecret;
			AwaitingResponseDate = document.AwaitingResponseDate;
			DocumentDate = document.DocumentDate;
			OutgoingDate = document.OutgoingDate;
			ProtocolDate = document.ProtocolDate;
			MonitoringDate = document.MonitoringDate;
			ApplicantType = document.ApplicantType;
			MonitoringType = document.MonitoringType;
			PriorityType = document.PriorityType;
			StateType = document.StateType;
			AppendixCount = document.AppendixCount;
			CopiesCount = document.CopiesCount;
			PageCount = document.PageCount == 0 ? (int?) null : document.PageCount;
			RepeatCount = document.RepeatCount;
			ApplicantAddress = document.ApplicantAddress;
			ApplicantEmail = document.ApplicantEmail;
			ApplicantName = document.ApplicantName;
			ApplicantPhone = document.ApplicantPhone;
			BlankNumber = document.BlankNumber;
			CorrespondentsInfo = document.CorrespondentsInfo;
			Number = document.Number;
			OutgoingNumber = document.OutgoingNumber;
			SortingNumber = document.SortingNumber;
			SortingOutgoingNumber = document.SortingOutgoingNumber;
			Note = document.Note;
			Summary = document.Summary ??  "";
			ProjectType = document.ProjectType;
			ResponsibleValue = document.ResponsibleValue;
            PriceSum = document.PriceSum;
		    RemarkId = document.RemarkId;
		    ParleyStartDate = document.ParleyStartDate ?? DateTime.Now;
		    ParleyEndDate = document.ParleyEndDate ?? DateTime.Now;
		    RemarkText1 = document.RemarkText1;
		    RemarkText2 = document.RemarkText2;
		    RemarkText3 = document.RemarkText3;

            CorrespondentsValue = document.CorrespondentsValue ?? "";
			MonitoringAuthorId = document.MonitoringAuthorId;
			MonitoringAuthorValue = document.MonitoringAuthorValue;
			MonitoringNote = document.MonitoringNote;

			DisplayName = document.DisplayName;
			RepeaterId = document.RepeaterId;

			SortNumber = document.SortNumber;

			FactExecutionDate = document.FactExecutionDate;
			FirstExecutionDate = document.FirstExecutionDate;
			ExecutionDate = document.ExecutionDate;
			Counters = document.Counters;

			ResolutionValue = document.ResolutionValue;
			OutgoingType = document.OutgoingType;

			Country = document.Country;
			Area = document.Area;
			Postcode = document.Postcode;
			Phone = document.Phone;
			Department = document.Department;
			City = document.City;
			Address = document.Address;
			NumberBill = document.NumberBill;
			Email = document.Email;
			ModifiedUser = document.ModifiedUser;

			DateDispatch = document.DateDispatch;
			DispatchNote = document.DispatchNote;
			Digest = document.Digest;
			Text = document.Text;
			Recipient = document.Recipient;
			InventoryId = document.InventoryId;
			FulfilledDate = document.FulfilledDate;
			Book = document.Book;
			Deed = document.Deed;
			Akt = document.Akt;
			AttachPath = document.AttachPath;
			ProjectType = document.ProjectType;
			MainDocumentId = document.MainDocumentId;
			MainTaskId = document.MainTaskId;
			if (string.IsNullOrEmpty(document.CreatedUserId))
			{
				document.CreatedUserValue = UserHelper.GetCurrentEmployee().DisplayName;
				document.CreatedUserId = UserHelper.GetCurrentEmployee().Id.ToString();
			}
        
			AdministrativeTypeDictionaryId = DictionaryHelper.GetItems(document.AdministrativeTypeDictionaryId, document.AdministrativeTypeDictionaryValue);
			ApplicantCategoryDictionaryId = DictionaryHelper.GetItems(document.ApplicantCategoryDictionaryId, document.ApplicantCategoryDictionaryValue);
			CauseCitizenDictionaryId = DictionaryHelper.GetItems(document.CauseCitizenDictionaryId, document.CauseCitizenDictionaryValue);
			CitizenCategoryDictionaryId = DictionaryHelper.GetItems(document.CitizenCategoryDictionaryId, document.CitizenCategoryDictionaryValue);
			CitizenResultDictionaryId = DictionaryHelper.GetItems(document.CitizenResultDictionaryId, document.CitizenResultDictionaryValue);
			CitizenTypeDictionaryId = DictionaryHelper.GetItems(document.CitizenTypeDictionaryId, document.CitizenTypeDictionaryValue);
			DocumentKindDictionaryId = DictionaryHelper.GetItems(document.DocumentKindDictionaryId, document.DocumentKindDictionaryValue);
			FormDeliveryDictionaryId = DictionaryHelper.GetItems(document.FormDeliveryDictionaryId, document.FormDeliveryDictionaryValue);
			FormSendingDictionaryId = DictionaryHelper.GetItems(document.FormSendingDictionaryId, document.FormSendingDictionaryValue);
			KatoDictionaryId = DictionaryHelper.GetItems(document.KatoDictionaryId, document.KatoDictionaryValue);
			LanguageDictionaryId = DictionaryHelper.GetItems(document.LanguageDictionaryId, document.LanguageDictionaryValue);
			NomenclatureDictionaryId = DictionaryHelper.GetItems(document.NomenclatureDictionaryId, document.NomenclatureDictionaryValue);
			QuestionDesignDictionaryId = DictionaryHelper.GetItems(document.QuestionDesignDictionaryId, document.QuestionDesignDictionaryValue);
            RegionId = DictionaryHelper.GetItems(document.RegionId, document.RegionValue);

            SigningFormDictionaryId = DictionaryHelper.GetItems(document.SigningFormDictionaryId, document.SigningFormDictionaryValue);
			CompleteDocumentsId = DictionaryHelper.GetItems(document.CompleteDocumentsId, document.CompleteDocumentsValue);
			EditDocumentsId = DictionaryHelper.GetItems(document.EditDocumentsId, document.EditDocumentsValue);
			RepealDocumentsId = DictionaryHelper.GetItems(document.RepealDocumentsId, document.RepealDocumentsValue);
			ReadersId = DictionaryHelper.GetItems(document.ReadersId, document.ReadersValue);
			ReadersId = DictionaryHelper.GetItems(document.ReadersId, document.ReadersValue);
			RecipientsId = DictionaryHelper.GetItems(document.RecipientsId, document.RecipientsValue);
			ExecutorsId = DictionaryHelper.GetItems(document.ExecutorsId, document.ExecutorsValue);
			AnswersId = DictionaryHelper.GetItems(document.AnswersId, document.AnswersValue);
			DestinationId = DictionaryHelper.GetItems(document.DestinationId, document.DestinationValue);
			AutoAnswersId = DictionaryHelper.GetItems(document.AutoAnswersId, document.AutoAnswersValue);
			AutoAnswersTempId = DictionaryHelper.GetItems(document.AutoAnswersTempId, document.AutoAnswersTempValue);
			AutoCompleteDocumentsId = DictionaryHelper.GetItems(document.AutoCompleteDocumentsId, document.AutoCompleteDocumentsValue);
			AutoEditDocumentsId = DictionaryHelper.GetItems(document.AutoEditDocumentsId, document.AutoEditDocumentsValue);
			AutoRepealDocumentsId = DictionaryHelper.GetItems(document.AutoRepealDocumentsId, document.AutoRepealDocumentsValue);
			DocumentDictionaryTypeId = DictionaryHelper.GetItems(document.DocumentDictionaryTypeId, document.DocumentDictionaryTypeValue);
			AgreementsId = DictionaryHelper.GetItems(document.AgreementsId, document.AgreementsValue);
			SignerId = DictionaryHelper.GetItems(document.SignerId, document.SignerValue);
			ResponsibleId = DictionaryHelper.GetItems(document.ResponsibleId, document.ResponsibleValue);
			CreatedUserId = DictionaryHelper.GetItems(document.CreatedUserId, document.CreatedUserValue);
			CorrespondentsId = DictionaryHelper.GetItems(document.CorrespondentsId, document.CorrespondentsValue);
			AttachFiles = UploadHelper.GetFilesInfo(document.AttachPath.ToString(), document.IsArchive);
		    IsArchive = document.IsArchive;

			CustomExecutorsId = DictionaryHelper.GetItems(document.ExecutorsId, document.ExecutorsValue);
		}

		public DocumentModel() {

		}
		private void ChangeCard(Document document) {
			document.AutoAwaitingResponseDate = document.AwaitingResponseDate;
			document.AutoDocumentDate = document.DocumentDate;
			document.AutoOutgoingDate = document.OutgoingDate;
			document.AutoProtocolDate = document.ProtocolDate;
			document.AutoMonitoringDate = document.MonitoringDate;
			document.AutoFactExecutionDate = document.FactExecutionDate;
			document.AutoFirstExecutionDate = document.FirstExecutionDate;
			document.AutoExecutionDate = document.ExecutionDate;
			if (document.DocumentDate.HasValue) {
				document.Year = document.DocumentDate.Value.Year;
				document.Month = document.DocumentDate.Value.Month;
				document.Day = document.DocumentDate.Value.Day;
			}
		}
		public Document GetDocument(Document document)
		{
			document.Id = Id;
			document.IsDeleted = IsDeleted;
			document.IsAdministrativeUse = IsAdministrativeUse;
			document.IsAwaitingResponse = IsAwaitingResponse;
			document.IsTradeSecret = IsTradeSecret;

			document.AwaitingResponseDate = AwaitingResponseDate;
			document.DocumentDate = DocumentDate;
			document.OutgoingDate = OutgoingDate;
			document.ProtocolDate = ProtocolDate;
			document.MonitoringDate = MonitoringDate;
			document.ApplicantType = ApplicantType;
			document.MonitoringType = MonitoringType;
			document.PriorityType = PriorityType;
			document.StateType = StateType;
			document.AppendixCount = AppendixCount;
			document.CopiesCount = CopiesCount;
			document.PageCount = PageCount.HasValue ? PageCount.Value : 0;
			document.RepeatCount = RepeatCount;
			document.ApplicantAddress = ApplicantAddress;
			document.ApplicantEmail = ApplicantEmail;
			document.ApplicantName = ApplicantName;
			document.ApplicantPhone = ApplicantPhone;
			document.BlankNumber = BlankNumber;
			document.CorrespondentsInfo = CorrespondentsInfo;
			//document.Number = Number;
			document.OutgoingNumber = OutgoingNumber;
			document.SortingNumber = SortingNumber;
			document.SortingOutgoingNumber = SortingOutgoingNumber;
			document.Note = Note;
			document.Summary = Summary;
			document.RepeaterId = RepeaterId;

		    document.RemarkId = RemarkId;
		    document.RemarkText1 = RemarkText1;
            document.RemarkText2 = RemarkText2;
            document.RemarkText3 = RemarkText3;
		    document.ParleyStartDate = ParleyStartDate;
		    document.ParleyEndDate = ParleyEndDate;

            document.ResponsibleValue = ResponsibleValue;
		    document.PriceSum = PriceSum;

            document.CorrespondentsValue = CorrespondentsValue;
			document.MonitoringAuthorId = MonitoringAuthorId;
			document.MonitoringAuthorValue = MonitoringAuthorValue;
			document.MonitoringNote = MonitoringNote;

			//document.DisplayName = DisplayName;

			//document.SortNumber = SortNumber;

			document.FactExecutionDate = FactExecutionDate;
			document.FirstExecutionDate = FirstExecutionDate;
			document.ExecutionDate = ExecutionDate;
			document.Counters = Counters;

			document.ResolutionValue = ResolutionValue;
			document.OutgoingType = OutgoingType;

			document.Country = Country;
			document.Area = Area;
			document.Postcode = Postcode;
			document.Phone = Phone;
			document.Department = Department;
			document.City = City;
			document.Address = Address;
			document.NumberBill = NumberBill;
			document.Email = Email;
			document.ModifiedUser = ModifiedUser;

			document.DateDispatch = DateDispatch;
			document.DispatchNote = DispatchNote;
			document.Digest = Digest;
			document.Text = Text;
			document.Recipient = Recipient;
			document.InventoryId = InventoryId;
			document.FulfilledDate = FulfilledDate;

			document.Book = Book;
			document.Deed = Deed;
			document.Akt = Akt;
			document.AttachPath = AttachPath;
			document.ProjectType = ProjectType;

			document.MainDocumentId = MainDocumentId;
			document.MainTaskId = MainTaskId;
            

            document.AdministrativeTypeDictionaryId = DictionaryHelper.GetItemsId(AdministrativeTypeDictionaryId);
			document.ApplicantCategoryDictionaryId = DictionaryHelper.GetItemsId(ApplicantCategoryDictionaryId);
			document.CauseCitizenDictionaryId = DictionaryHelper.GetItemsId(CauseCitizenDictionaryId);
			document.CitizenCategoryDictionaryId = DictionaryHelper.GetItemsId(CitizenCategoryDictionaryId);
			document.CitizenResultDictionaryId = DictionaryHelper.GetItemsId(CitizenResultDictionaryId);
			document.CitizenTypeDictionaryId = DictionaryHelper.GetItemsId(CitizenTypeDictionaryId);
			document.DocumentKindDictionaryId = DictionaryHelper.GetItemsId(DocumentKindDictionaryId);
			document.FormDeliveryDictionaryId = DictionaryHelper.GetItemsId(FormDeliveryDictionaryId);
			document.FormSendingDictionaryId = DictionaryHelper.GetItemsId(FormSendingDictionaryId);
			document.KatoDictionaryId = DictionaryHelper.GetItemsId(KatoDictionaryId);
			document.LanguageDictionaryId = DictionaryHelper.GetItemsId(LanguageDictionaryId);
			document.NomenclatureDictionaryId = DictionaryHelper.GetItemsId(NomenclatureDictionaryId);
			document.QuestionDesignDictionaryId = DictionaryHelper.GetItemsId(QuestionDesignDictionaryId);
            document.RegionId = DictionaryHelper.GetItemsId(RegionId);

            document.SigningFormDictionaryId = DictionaryHelper.GetItemsId(SigningFormDictionaryId);
			document.CompleteDocumentsId = DictionaryHelper.GetItemsId(CompleteDocumentsId);
			document.EditDocumentsId = DictionaryHelper.GetItemsId(EditDocumentsId);
			document.RepealDocumentsId = DictionaryHelper.GetItemsId(RepealDocumentsId);
			document.ReadersId = DictionaryHelper.GetItemsId(ReadersId);
			document.RecipientsId = DictionaryHelper.GetItemsId(RecipientsId);
			document.AnswersId = DictionaryHelper.GetItemsId(AnswersId);
			document.DestinationId = DictionaryHelper.GetItemsId(DestinationId);
			document.ExecutorsId = DictionaryHelper.GetItemsId(ExecutorsId);
			document.DocumentDictionaryTypeId = DictionaryHelper.GetItemsId(DocumentDictionaryTypeId);
			document.AgreementsId = DictionaryHelper.GetItemsId(AgreementsId);
			document.SignerId = DictionaryHelper.GetItemsId(SignerId);
			document.ResponsibleId = DictionaryHelper.GetItemsId(ResponsibleId);
			document.CreatedUserId = DictionaryHelper.GetItemsId(CreatedUserId);
			document.CorrespondentsId = DictionaryHelper.GetItemsId(CorrespondentsId);

			document.AdministrativeTypeDictionaryValue = DictionaryHelper.GetItemsName(AdministrativeTypeDictionaryId);
			document.ApplicantCategoryDictionaryValue = DictionaryHelper.GetItemsName(ApplicantCategoryDictionaryId);
			document.CauseCitizenDictionaryValue = DictionaryHelper.GetItemsName(CauseCitizenDictionaryId);
			document.CitizenCategoryDictionaryValue = DictionaryHelper.GetItemsName(CitizenCategoryDictionaryId);
			document.CitizenResultDictionaryValue = DictionaryHelper.GetItemsName(CitizenResultDictionaryId);
			document.CitizenTypeDictionaryValue = DictionaryHelper.GetItemsName(CitizenTypeDictionaryId);
			document.DocumentKindDictionaryValue = DictionaryHelper.GetItemsName(DocumentKindDictionaryId);
			document.FormDeliveryDictionaryValue = DictionaryHelper.GetItemsName(FormDeliveryDictionaryId);
			document.FormSendingDictionaryValue = DictionaryHelper.GetItemsName(FormSendingDictionaryId);
			document.KatoDictionaryValue = DictionaryHelper.GetItemsName(KatoDictionaryId);
			document.LanguageDictionaryValue = DictionaryHelper.GetItemsName(LanguageDictionaryId);
			document.NomenclatureDictionaryValue = DictionaryHelper.GetItemsName(NomenclatureDictionaryId);
			document.QuestionDesignDictionaryValue = DictionaryHelper.GetItemsName(QuestionDesignDictionaryId);
            document.RegionValue = DictionaryHelper.GetItemsName(RegionId);

            document.SigningFormDictionaryValue = DictionaryHelper.GetItemsName(SigningFormDictionaryId);
			document.CompleteDocumentsValue = DictionaryHelper.GetItemsName(CompleteDocumentsId);
			document.EditDocumentsValue = DictionaryHelper.GetItemsName(EditDocumentsId);
			document.RepealDocumentsValue = DictionaryHelper.GetItemsName(RepealDocumentsId);
			document.ReadersValue = DictionaryHelper.GetItemsName(ReadersId);
			document.RecipientsValue = DictionaryHelper.GetItemsName(RecipientsId);
			document.AnswersValue = DictionaryHelper.GetItemsName(AnswersId);
			document.DestinationValue = DictionaryHelper.GetItemsName(DestinationId);
			document.ExecutorsValue = DictionaryHelper.GetItemsName(ExecutorsId);
			document.DocumentDictionaryTypeValue = DictionaryHelper.GetItemsName(DocumentDictionaryTypeId);
			document.AgreementsValue = DictionaryHelper.GetItemsName(AgreementsId);
			document.SignerValue = DictionaryHelper.GetItemsName(SignerId);
			document.ResponsibleValue = DictionaryHelper.GetItemsName(ResponsibleId);
			document.CreatedUserValue = DictionaryHelper.GetItemsName(CreatedUserId);
			document.CorrespondentsValue = DictionaryHelper.GetItemsName(CorrespondentsId);
			ChangeCard(document);
			return document;
		}

		#region reference
		public List<Item> AdministrativeTypeDictionaryId { get; set; }
		public List<Item> ApplicantCategoryDictionaryId { get; set; }
		public List<Item> CauseCitizenDictionaryId { get; set; }
		public List<Item> CitizenCategoryDictionaryId { get; set; }
		public List<Item> CitizenResultDictionaryId { get; set; }
		public List<Item> CitizenTypeDictionaryId { get; set; }
		public List<Item> DocumentKindDictionaryId { get; set; }
		public List<Item> FormDeliveryDictionaryId { get; set; }
		public List<Item> FormSendingDictionaryId { get; set; }
		public List<Item> KatoDictionaryId { get; set; }
		public List<Item> LanguageDictionaryId { get; set; }

		public List<Item> NomenclatureDictionaryId { get; set; }
		public List<Item> QuestionDesignDictionaryId { get; set; }
        public List<Item> RegionId { get; set; }

        public List<Item> SigningFormDictionaryId { get; set; }
		public List<Item> CompleteDocumentsId { get; set; }
		public List<Item> EditDocumentsId { get; set; }
		public List<Item> RepealDocumentsId { get; set; }

		public List<Item> ReadersId { get; set; }
		public List<Item> RecipientsId { get; set; }

		public List<Item> AnswersId { get; set; }
		public List<Item> DestinationId { get; set; }
		public List<Item> ExecutorsId { get; set; }

		public List<Item> AutoAnswersId { get; set; }
		public List<Item> AutoAnswersTempId { get; set; }

		public List<Item> AutoCompleteDocumentsId { get; set; }
		public List<Item> AutoEditDocumentsId { get; set; }
		public List<Item> AutoRepealDocumentsId { get; set; }
		public List<Item> DocumentDictionaryTypeId { get; set; }
		public List<Item> AgreementsId { get; set; }
		public List<Item> SignerId { get; set; }
		public List<Item> ResponsibleId { get; set; }
		public List<Item> CreatedUserId { get; set; }
		public List<Item> CorrespondentsId { get; set; }

		#endregion
		public Guid Id { get; set; }
		public bool IsDeleted { get; set; }
		public bool IsAdministrativeUse { get; set; }
		public bool IsAwaitingResponse { get; set; }
		public bool IsTradeSecret { get; set; }
		[JsonProperty]
		public DateTime? AwaitingResponseDate { get; set; }
		[JsonProperty]
		public DateTime? DocumentDate { get; set; }
			[JsonProperty]
		public DateTime? OutgoingDate { get; set; }
			[JsonProperty]
		public DateTime? ProtocolDate { get; set; }
			[JsonProperty]
		public DateTime? MonitoringDate { get; set; }
		public int ApplicantType { get; set; }
		public int DocumentType { get; set; }
		public int MonitoringType { get; set; }
		public int PriorityType { get; set; }
		public int StateType { get; set; }
		public int AppendixCount { get; set; }
		public int CopiesCount { get; set; }
		public int? PageCount { get; set; }
		public int RepeatCount { get; set; }
		public string ApplicantAddress { get; set; }
		public string ApplicantEmail { get; set; }
		public string ApplicantName { get; set; }
		public string ApplicantPhone { get; set; }
		public string BlankNumber { get; set; }
		public string CorrespondentsInfo { get; set; }
		public string Number { get; set; }
		public string OutgoingNumber { get; set; }
		public string SortingNumber { get; set; }
		public string SortingOutgoingNumber { get; set; }
		public string Note { get; set; }
		public string Summary { get; set; }
        public Guid? RepeaterId { get; set; }
		public Guid? AttachmentId { get; set; }
		public Guid? ResolutionId { get; set; }
		
		public string RegistratorId { get; set; }
		public string RegistratorValue { get; set; }
		public string ResponsibleValue { get; set; }


		public string CorrespondentsValue { get; set; }
		public string MonitoringAuthorId { get; set; }
		public string MonitoringAuthorValue { get; set; }
		public string MonitoringNote { get; set; }

		public string DisplayName { get; set; }

		public int SortNumber { get; set; }
			[JsonProperty]
		public DateTime? FactExecutionDate { get; set; }
			[JsonProperty]
		public DateTime? FirstExecutionDate { get; set; }

		[JsonProperty]
		public DateTime? ExecutionDate { get; set; }
		public string Counters { get; set; }

		public string ResolutionValue { get; set; }
		public int OutgoingType { get; set; }

		public string OwnerId { get; set; }
		public string OwnerValue { get; set; }
		public string Country { get; set; }
		public string Area { get; set; }
		public string Postcode { get; set; }
		public string Phone { get; set; }
		public string Department { get; set; }
		public string City { get; set; }
		public string Address { get; set; }
		public string NumberBill { get; set; }
		public string Email { get; set; }
		public string ModifiedUser { get; set; }
			[JsonProperty]
		public DateTime? DateDispatch { get; set; }
		public string DispatchNote { get; set; }
		public string Digest { get; set; }
		public string Text { get; set; }
		public string Recipient { get; set; }
		public string Book { get; set; }
		public string Deed { get; set; }
		public string Akt { get; set; }
		public string AttachPath { get; set; }
        public Nullable<decimal> PriceSum { get; set; }
        public int? RemarkId { get; set; }
        public string RemarkText1 { get; set; }
        public string RemarkText2 { get; set; }
        public string RemarkText3 { get; set; }
                
        public DateTime ParleyStartDate { get; set; }
        public DateTime ParleyEndDate { get; set; }

        public Guid? MainTaskId { get; set; }
		public Guid? MainDocumentId { get; set; }
		public int ProjectType { get; set; }
		public bool IsArchive { get; set; }
		public int? InventoryId { get; set; }
			[JsonProperty]
		public DateTime? FulfilledDate { get; set; }

		public List<UploadInitialFile> AttachFiles { get; set; }

		public List<Item> CustomExecutorsId { get; set; }

		public string StateTypeValue {
			get {
				switch (StateType) {
					case 0:
						return Messages.DocumentState0;
					case 1:
						return Messages.DocumentState1;
					case 2:
						return Messages.DocumentState2;
					case 3:
						return Messages.DocumentState3;
					case 4:
						return Messages.DocumentState4;
					case 5:
						return Messages.DocumentState5;
					case 6:
						return Messages.DocumentState6;
					case 7:
						return Messages.DocumentState7;
					case 8:
						return Messages.DocumentState8;
					case 9:
						return Messages.DocumentState9;
				}
				return string.Empty;
			}
		}
	}

	public class Item : IEquatable<Item>
	{
		public string Id { get; set; }
		public string Name{ get; set; }

		public bool Equals(Item other) {

			//Check whether the compared object is null. 
			if (Object.ReferenceEquals(other, null))
				return false;

			//Check whether the compared object references the same data. 
			if (Object.ReferenceEquals(this, other))
				return true;

			//Check whether the products' properties are equal. 
			return Id.Equals(other.Id) && Name.Equals(other.Name);
		}

		// If Equals() returns true for a pair of objects  
		// then GetHashCode() must return the same value for these objects. 

		public override int GetHashCode() {

			//Get hash code for the Name field if it is not null. 
			int hashProductName = Name == null ? 0 : Name.GetHashCode();

			//Get hash code for the Code field. 
			int hashProductCode = Id.GetHashCode();

			//Calculate the hash code for the product. 
			return hashProductName ^ hashProductCode;
		}
	}
}