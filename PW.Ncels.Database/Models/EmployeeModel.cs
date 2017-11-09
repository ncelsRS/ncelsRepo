using System;
using System.Collections.Generic;
using PW.Ncels.Database.DataModel;
using PW.Ncels.Database.Helpers;

namespace PW.Ncels.Database.Models
{
	public class EmployeeModel
	{
		public EmployeeModel()
		{
		}

		public EmployeeModel(Employee employee)
		{
			Id = employee.Id;
			LastName = employee.LastName;
			FirstName = employee.FirstName;
			MiddleName = employee.MiddleName;
			FullName = employee.FullName;
			ShortName = employee.ShortName;
			Sex = employee.Sex;
			Email = employee.Email;
			Phone = employee.Phone;
			Login = employee.Login;
			IsLockedOut = employee.IsLockedOut;
			LastLoginDate = employee.LastLoginDate;
			DeputyId = DictionaryHelper.GetItems(employee.DeputyId, employee.DeputyValue);
			AssistantsId = DictionaryHelper.GetItems(employee.AssistantsId, employee.AssistantsValue);
			Cabinet = employee.Cabinet;
			AttachFiles = UploadHelper.GetFilesInfo(employee.Id.ToString(), false);
			Position = employee.Position != null ? employee.Position.Name : string.Empty;
			Number = employee.Number;
			Birthday = employee.Birthday;
			Birthplace = employee.Birthplace;
			UlNumber = employee.UlNumber;
			UlDate = employee.UlDate;
			UlOwner = employee.UlOwner;
			Iin = employee.Iin;
			PlaceLive = employee.PlaceLive;
			PlaceRegistration = employee.PlaceRegistration;
			PhoneHome = employee.PhoneHome;
			PhoneMobile = employee.PhoneMobile;
			FamilyStatus = employee.FamilyStatus;
			Families = employee.Families;
			Education = employee.Education;
			ExperienceTotal = employee.ExperienceTotal;
			ExperienceSpec = employee.ExperienceSpec;
			IsDegree = Convert.ToInt32(employee.IsDegree);
			DegreeDate = employee.DegreeDate;
			DegreeSpec = employee.DegreeSpec;
			MilitaryType = employee.MilitaryType;
			MilitaryCategory = employee.MilitaryCategory;
			MilitarySostavId = DictionaryHelper.GetItems(employee.MilitarySostavId, employee.MilitarySostav);
			MilitaryRankId = DictionaryHelper.GetItems(employee.MilitaryRankId, employee.MilitaryRank);
			MilitaryVus = employee.MilitaryVus;
			MilitaryLocation = employee.MilitaryLocation;
			MilitaryLastDate = employee.MilitaryLastDate;
			DateFile = employee.DateFile;
			EducationNumber = employee.EducationNumber;
			EducationDate = employee.EducationDate;
			EducationSpec = employee.EducationSpec;
			EducationQual = employee.EducationQual;
			PositionId = employee.PositionId;
			NationalityId = DictionaryHelper.GetItems(employee.NationalityId, employee.NationalityValue);
			CauseLayoffsId = DictionaryHelper.GetItems(employee.CauseLayoffsId, employee.CauseLayoffsValue);
			DegreeNameId = DictionaryHelper.GetItems(employee.DegreeNameId, employee.DegreeName);
			QualificationCategoryId = DictionaryHelper.GetItems(employee.QualificationCategoryId, employee.QualificationCategoryValue);
			IsAcademic = Convert.ToInt32(employee.IsAcademic);
			AcademicDate = employee.AcademicDate;
			AcademicSpec = employee.AcademicSpec;
			AcademicNameId = DictionaryHelper.GetItems(employee.AcademicNameId, employee.AcademicName);
			Education2 = employee.Education2;
			EducationNumber2 = employee.EducationNumber2;
			EducationDate2 = employee.EducationDate2;
			EducationSpec2 = employee.EducationSpec2;
			EducationQual2 = employee.EducationQual2;
			Education3 = employee.Education3;
			EducationNumber3 = employee.EducationNumber3;
			EducationDate3 = employee.EducationDate3;
			EducationSpec3 = employee.EducationSpec3;
			EducationQual3 = employee.EducationQual3;
			TerminationDate = employee.TerminationDate;
			MilitaryCategory = employee.MilitaryCategory;
		}

		public Employee GetEmployee(Employee employee)
		{
			employee.LastName = LastName;
			employee.FirstName = FirstName;
			employee.MiddleName = MiddleName;
			employee.FullName = FullName;
			employee.ShortName = ShortName;
			employee.Sex = Sex;
			employee.Email = Email;
			employee.Phone = Phone;
			employee.Login = Login;
			employee.IsLockedOut = IsLockedOut;
			employee.LastLoginDate = LastLoginDate;
			employee.Cabinet = Cabinet;
			employee.DeputyId = DictionaryHelper.GetItemsId(DeputyId);
			employee.DeputyValue = DictionaryHelper.GetItemsName(DeputyId);
			employee.AssistantsId = DictionaryHelper.GetItemsId(AssistantsId);
			employee.AssistantsValue = DictionaryHelper.GetItemsName(AssistantsId);
			employee.Number = Number;
			employee.Birthday = Birthday;
			employee.Birthplace = Birthplace;
			employee.UlNumber = UlNumber;
			employee.UlDate = UlDate;
			employee.UlOwner = UlOwner;
			employee.Iin = Iin;
			employee.PlaceLive = PlaceLive;
			employee.PlaceRegistration = PlaceRegistration;
			employee.PhoneHome = PhoneHome;
			employee.PhoneMobile = PhoneMobile;
			employee.FamilyStatus = FamilyStatus;
			employee.Families = Families;
			employee.Education = Education;
			employee.ExperienceTotal = ExperienceTotal;
			employee.ExperienceSpec = ExperienceSpec;
			employee.IsDegree = Convert.ToBoolean(IsDegree);
			employee.DegreeDate = DegreeDate;
			employee.DegreeSpec = DegreeSpec;
			employee.MilitaryType = MilitaryType;
			employee.MilitaryCategory = MilitaryCategory;
			employee.MilitarySostav = DictionaryHelper.GetItemsName(MilitarySostavId);
			employee.MilitaryRank = DictionaryHelper.GetItemsName(MilitaryRankId);
			employee.MilitaryVus = MilitaryVus;
			employee.MilitaryLocation = MilitaryLocation;
			employee.MilitaryLastDate = MilitaryLastDate;
			employee.DateFile = DateFile;
			employee.EducationNumber = EducationNumber;
			employee.EducationDate = EducationDate;
			employee.EducationSpec = EducationSpec;
			employee.EducationQual = EducationQual;
			employee.PositionId = PositionId;
			employee.Login = Login;
			employee.NationalityId = DictionaryHelper.GetItemsId(NationalityId);
			employee.NationalityValue = DictionaryHelper.GetItemsName(NationalityId);
			employee.CauseLayoffsId = DictionaryHelper.GetItemsId(CauseLayoffsId);
			employee.CauseLayoffsValue = DictionaryHelper.GetItemsName(CauseLayoffsId);
			employee.QualificationCategoryValue = DictionaryHelper.GetItemsName(QualificationCategoryId);
			employee.DegreeName = DictionaryHelper.GetItemsName(DegreeNameId);
			employee.IsAcademic = Convert.ToBoolean(IsAcademic);
			employee.AcademicDate = AcademicDate;
			employee.AcademicSpec = AcademicSpec;
			employee.AcademicName = DictionaryHelper.GetItemsName(AcademicNameId);
			employee.Education2 = Education2;
			employee.EducationNumber2 = EducationNumber2;
			employee.EducationDate2 = EducationDate2;
			employee.EducationSpec2 = EducationSpec2;
			employee.EducationQual2 = EducationQual2;
			employee.Education3 = Education3;
			employee.EducationNumber3 = EducationNumber3;
			employee.EducationDate3 = EducationDate3;
			employee.EducationSpec3 = EducationSpec3;
			employee.EducationQual3 = EducationQual3;
			employee.TerminationDate = TerminationDate;
			employee.MilitaryCategory = MilitaryCategory;
			employee.MilitarySostavId = DictionaryHelper.GetItemsId(MilitarySostavId);
			employee.MilitaryRankId = DictionaryHelper.GetItemsId(MilitaryRankId);
			employee.AcademicNameId = DictionaryHelper.GetItemsId(AcademicNameId);
			employee.DegreeNameId = DictionaryHelper.GetItemsId(DegreeNameId);
			employee.QualificationCategoryId = DictionaryHelper.GetItemsId(QualificationCategoryId);
			return employee;
		}

		public System.Guid Id { get; set; }
		public string LastName { get; set; }
		public string FirstName { get; set; }
		public string MiddleName { get; set; }
		public string FullName { get; set; }
		public string ShortName { get; set; }
		public int Sex { get; set; }
		public string Email { get; set; }
		public string Phone { get; set; }
		public string Login { get; set; }
		public Nullable<bool> IsLockedOut { get; set; }
		public Nullable<System.DateTime> LastLoginDate { get; set; }
		public List<Item> DeputyId { get; set; }
		public List<Item> AssistantsId { get; set; }
		public string Cabinet { get; set; }
		public List<UploadInitialFile> AttachFiles { get; set; }
		public string Position { get; set; }
		public string Number { get; set; }
		public DateTime? Birthday { get; set; }
		public string Birthplace { get; set; }
		public string UlNumber { get; set; }
		public DateTime? UlDate { get; set; }
		public string UlOwner { get; set; }
		public string Iin { get; set; }
		public string PlaceLive { get; set; }
		public string PlaceRegistration { get; set; }
		public string PhoneHome { get; set; }
		public string PhoneMobile { get; set; }
		public int FamilyStatus { get; set; }
		public string Families { get; set; }
		public string Education { get; set; }
		public int ExperienceTotal { get; set; }
		public int ExperienceSpec { get; set; }
		public int IsDegree { get; set; }
		public DateTime? DegreeDate { get; set; }
		public string DegreeSpec { get; set; }
		public int MilitaryType { get; set; }
		public string MilitaryCategory { get; set; }
		public List<Item> MilitarySostavId { get; set; }
		public List<Item> MilitaryRankId { get; set; }
		public string MilitaryVus { get; set; }
		public string MilitaryLocation { get; set; }
		public DateTime? MilitaryLastDate { get; set; }
		public DateTime? DateFile { get; set; }
		public string EducationNumber { get; set; }
		public DateTime? EducationDate { get; set; }
		public string EducationSpec { get; set; }
		public string EducationQual { get; set; }
		public Guid? PositionId { get; set; }
		public List<Item> NationalityId { get; set; }
		public List<Item> CauseLayoffsId { get; set; }
		public int IsAcademic { get; set; }
		public DateTime? AcademicDate { get; set; }
		public string AcademicSpec { get; set; }
		public string Education2 { get; set; }
		public string EducationNumber2 { get; set; }
		public DateTime? EducationDate2 { get; set; }
		public string EducationSpec2 { get; set; }
		public string EducationQual2 { get; set; }
		public string Education3 { get; set; }
		public string EducationNumber3 { get; set; }
		public DateTime? EducationDate3 { get; set; }
		public string EducationSpec3 { get; set; }
		public string EducationQual3 { get; set; }
		public DateTime? TerminationDate { get; set; }
		public List<Item> AcademicNameId { get; set; }
		public List<Item> DegreeNameId { get; set; }
		public List<Item> QualificationCategoryId { get; set; }

	}
}