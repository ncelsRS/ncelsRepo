using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using PW.Ncels.Database.DataModel;
using PW.Ncels.Database.DataModel;
using Document = PW.Ncels.Database.DataModel.Document;

namespace PW.Ncels.Database.Helpers {
	public static class Registrator {

		/// <summary>
		/// Регистрация повторных документов
		/// </summary>
		/// <param name="document"></param>
		public static void SetRepeaterNumber(Document document) {
			ncelsEntities context = UserHelper.GetCn();
			int number = 0;
			string strNumber = string.Empty;
			if (document.RepeaterId.HasValue) {
				Document repeatDocument = context.Documents.FirstOrDefault(x => x.Id == document.RepeaterId);
				int repeatCount =
					context.Documents.Count(
						x => x.RepeaterId == document.RepeaterId.Value && x.Id != document.Id && x.IsDeleted == false) + 1;
				if (repeatDocument != null &&
				    (repeatDocument.DocumentType != 3 || (repeatDocument.DocumentType == 3 && repeatDocument.ProjectType == 3))) {
					strNumber = string.Format("{0},{1}", repeatDocument.Number, repeatCount + 1);
					number = repeatDocument.SortNumber;
				}
				if (repeatDocument != null && repeatDocument.DocumentType == 3 && repeatDocument.ProjectType == 4) {
					string tempNumber = repeatDocument.Number;
					int tempIndex = tempNumber.IndexOf(",", StringComparison.CurrentCulture);

					if (tempIndex < tempNumber.Length) {
						tempNumber = tempNumber.Substring(0, tempIndex);
						repeatCount = repeatCount + 1;
					}

					strNumber = string.Format("{0},{1}", tempNumber, repeatCount);
					number = repeatDocument.SortNumber;
				}
				document.Number = strNumber;
				document.DocumentDate = DateTime.Now;
				document.SortNumber = number;
				document.SortingNumber = BuildFormattedNumber(strNumber);
				document.DisplayName = BuildDisplayName(document);
			}
		}

		/// <summary>
		/// Регистрация документов, кроме входящих (они регятся в БД)
		/// </summary>
		/// <param name="document"></param>
		public static void SetNumber(Document document) {
			ncelsEntities context = UserHelper.GetCn();
			int number = 0;
			string strNumber = string.Empty;
			if (document.RepeaterId.HasValue) {
				Document repeatDocument = context.Documents.FirstOrDefault(x => x.Id == document.RepeaterId);
				int repeatCount = context.Documents.Count(x => x.RepeaterId == document.RepeaterId.Value && x.Id != document.Id) + 1;
				if (repeatDocument != null &&
				    (repeatDocument.DocumentType != 3 || (repeatDocument.DocumentType == 3 && repeatDocument.ProjectType == 3))) {
					strNumber = string.Format("{0},{1}", repeatDocument.Number, repeatCount + 1);
					number = repeatDocument.SortNumber;
				}
				if (repeatDocument != null && repeatDocument.DocumentType == 3 && repeatDocument.ProjectType == 4) {
					string tempNumber = repeatDocument.Number;
					int tempIndex = tempNumber.IndexOf(",", StringComparison.CurrentCulture);

					if (tempIndex < tempNumber.Length) {
						tempNumber = tempNumber.Substring(0, tempIndex);
						repeatCount = repeatCount + 1;
					}
					strNumber = string.Format("{0},{1}", tempNumber, repeatCount);
					number = repeatDocument.SortNumber;
				}
				if (repeatDocument != null && repeatDocument.DocumentType == 6) {
					strNumber = string.Format("{0},{1}", repeatDocument.Number, repeatCount);
					number = repeatDocument.SortNumber;
				}
			}
			else {
				number = GetNumber(document, context);
				switch (document.DocumentType) {
					case 0:
				        strNumber = number + String.Empty;
                        break;
					case 1:
						Guid id = Guid.Parse(document.NomenclatureDictionaryId);
						Dictionary dictionary =
							context.Dictionaries.FirstOrDefault(o => o.Id == id);
						string pref = string.Empty;
						if (dictionary != null) {
							pref = dictionary.Code;
						}
						if (document.OutgoingType == 0) {
							strNumber = string.Format("{1}/{0}-И", number, pref);
						}
						else {
							strNumber = string.Format("{1}/{0}", number, pref);
						}
						break;
					case 2:
						//физическое лицо
						if (document.ApplicantType == 0) {
							if (document.ApplicantCategoryDictionaryValue == "Аноним") {
								strNumber = string.Format("Д/А-{0}", number);
							}
							else if (document.ApplicantCategoryDictionaryValue == "Коллектив") {
								strNumber = string.Format("ҰЖ-{0}", number);
							}
							else {
								strNumber = string.Format("ЖТ-{0}-{1}", GetCodeCitizenName(document.CorrespondentsInfo), number);
							}
						}
						else {
							//юридические лица
							strNumber = string.Format("ЗТ-{0}-{1}", GetCodeCitizenName(document.CorrespondentsInfo), number);
						}
						break;
					case 3:
						// Приказы
						if (document.ProjectType == 3) {
							switch (document.AdministrativeTypeDictionaryId.ToUpper()) {
								case "0BB8D6CB-2BDA-483A-9258-4E951B1329C8":
									strNumber = string.Format("{0}-ж", number);
									break;
								case "91E8A59D-497C-470F-ACB4-71BD13E49823":
									strNumber = string.Format("{0}-д", number);
									break;
								case "6099492A-0761-4ACC-A39C-A35F1749E313":
									strNumber = string.Format("{0}-iс", number);
									break;
								default:
									strNumber = string.Format("{0}", number);
									break;
							}
						}
						// Приказы по основной деятельности
						if (document.ProjectType == 6) {
							strNumber = string.Format("{0}", number);
						}
						// Протокола
						if (document.ProjectType == 4) {
							strNumber = string.Format("{0},1", number);
						}
						break;
					case 4:
						strNumber = string.Format("П-{0}", number);
						break;
					case 5:
						if (document.ApplicantType == 0) {
							Guid userId = Guid.Parse(document.CreatedUserId);
							Employee employee = context.Employees.Include("Position.Parent").FirstOrDefault(d => d.Id == userId);
							string prefInner = string.Empty;
							if (employee?.Position?.Parent != null)
								prefInner = employee.Position.Parent.Code;
							strNumber = string.Format("{0}-{1}", prefInner, number);
						}
						if (document.ApplicantType == 1)
							strNumber = string.Format("{0}", number);
						break;
					case 6:
						strNumber = string.Format("{0}", number);
						break;
					default:
						throw new ArgumentOutOfRangeException("document");
				}
			}
			document.Number = strNumber;
			document.DocumentDate = DateTime.Now;
			document.SortNumber = number;
			document.SortingNumber = BuildFormattedNumber(strNumber);
			document.DisplayName = BuildDisplayName(document);
		}

		private static int GetNumber(Document document, ncelsEntities context) {
			IList<Setting> settings = context.Settings.Where(x => x.Type == "Counts").ToList();
			int value;
			string countName = string.Empty;
			switch (document.DocumentType) {
				case 0:
					countName = "IncCount";
					break;
				case 1:
					if (document.OutgoingType == 0)
						countName = "OutCount";
					else
						countName = "AnswerOutCount";
					break;
				case 2:
					//if (document.ApplicantType == 0)
					//	countName = "Cit0Count";
					//else
						countName = "CitCount";
					break;
				case 3:
					if (document.ProjectType == 3)
						countName = document.AdministrativeTypeDictionaryId.ToUpper();
					else if (document.ProjectType == 6)
						countName = "PrtMainCount";
					else
						countName = "PrtCount";
					break;
				case 4:
					countName = "PrjCount";
					break;
				case 5:
					if (document.ApplicantType == 0) {
						Guid userId = Guid.Parse(document.CreatedUserId);
						Employee employee = context.Employees.Include("Position.Parent").FirstOrDefault(d => d.Id == userId);
						if (employee?.Position?.Parent != null)
							countName = employee.Position.Parent.Id.ToString();
					}
					if (document.ApplicantType == 1)
						countName = "MainInnerDoc";
					break;
				case 6:
					countName = "ContractCount";
					break;
			}
			Guid orgId = UserHelper.GetCurrentEmployee().OrganizationId;
			Setting counter = settings.FirstOrDefault(x => x.UniqueName == countName && x.UserId == orgId);
			if (counter != null) {
				value = int.Parse(counter.Value) + 1;
				counter.Value = value.ToString(CultureInfo.InvariantCulture);
			}
			else {
				Setting newCounter = new Setting {
					Id = Guid.NewGuid(),
					Name = document.DocumentType.ToString(),
					Type = "Counts",
					Rank = 0,
					Value = "1",
					UniqueName = countName,
					UserId = orgId
				};
				context.Settings.Add(newCounter);
				value = 1;
			}
			context.SaveChanges();
			return value;
		}

		private static string BuildDisplayName(Document document) {
			string number = "Б/Н";
			string date = string.Empty;

			if (!string.IsNullOrEmpty(document.Number))
				number = document.Number;

			if (document.DocumentDate != null)
				date = string.Format("{0:dd.MM.yyyy}", document.DocumentDate);

			return string.Format("{0} от {1}", number, date);
		}

		private static string GetCodeCitizenName(string citizenName) {
			return !string.IsNullOrEmpty(citizenName.Trim())
				? new string(new[] {citizenName.Trim()[0]}).ToUpper()
				: "Ан";
		}

		public static string BuildFormattedNumber(string number) {
			string formattedNumber = string.Empty;
			try {
				Regex regex = new Regex("([0-9]+)");
				if (!string.IsNullOrEmpty(number)) {
					string[] builder = regex.Split(number);
					Regex regexNumber = new Regex("(^[0-9]+$)");
					foreach (string s in builder) {
						string part = s;
						if (regexNumber.IsMatch(s))
							part = Int64.Parse(s).ToString("000000000");
						formattedNumber += part;
					}
				}
				return formattedNumber;
			}
			catch (Exception) {
				return number;
			}
		}
        

        /// <summary>
        /// ПОлучить номер из настроек
        /// </summary>
        /// <param name="settingType">Тип реестра</param>
        /// <returns></returns>
        public static int GetNumber(string settingType)
        {
            ncelsEntities context = new ncelsEntities();
            IList<Setting> settings = context.Settings.Where(x => x.Type == "Counts" && x.UniqueName == settingType).ToList();
            int value;
            string countName = settingType;

            Guid orgId = UserHelper.GetCurrentEmployee().OrganizationId;
            Setting counter = settings.FirstOrDefault(x => x.UniqueName == countName && x.UserId == orgId);
            if (counter != null)
            {
                value = int.Parse(counter.Value) + 1;
                counter.Value = value.ToString(CultureInfo.InvariantCulture);
            }
            else
            {
                Setting newCounter = new Setting
                {
                    Id = Guid.NewGuid(),
                    Name = settingType,
                    Type = "Counts",
                    Rank = 0,
                    Value = "1",
                    UniqueName = countName,
                    UserId = orgId
                };
                context.Settings.Add(newCounter);
                value = 1;
            }
            context.SaveChanges();
            return value;
        }


    }
}