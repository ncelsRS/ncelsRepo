using System;

namespace PW.Ncels.Database.Models {
	public class TaskListItemModel {
		public Guid TaskId { get; set; }

		public string Number { get; set; }
		public string Summary { get; set; }

		

		public Guid DocumentId { get; set; }

		public string DocumentNumber { get; set; }

		public int ProjectType { get; set; }

		public DateTime? DocumentDate { get; set; }

		public string DocumentDateStr {
			get {
				if (DocumentDate != null)
					return
						DocumentDate.Value.ToShortDateString();
				return string.Empty;
			}
		}

		public DateTime? ExecutionDate { get; set; }

		public string ExecutionDateStr {
			get {
				if (ExecutionDate != null)
					return
						ExecutionDate.Value.ToShortDateString();
				return string.Empty;
			}
		}

		public string Author { get; set; }

		public string Executor { get; set; }

		public string Text { get; set; }

		public int Type { get; set; }

		public int TypeEx { get; set; }

		public int State { get; set; }

		public bool IsResponsible { get; set; }

		public DateTime? CreatedDate { get; set; }
        public string DocumentDictionaryTypeId { get; set; }
        public string DocumentDictionaryTypeValue { get; set; }
        public Nullable<decimal> PriceSum { get; set; }

        public string TimeTask {
			get {
				TimeSpan timetask = (DateTime.Now - CreatedDate.Value);
				if (timetask.TotalMinutes < 60)
					return string.Format("{0} мин назад", (int)timetask.TotalMinutes);
				if (timetask.TotalHours < 24)
					return string.Format("{0} ч назад", (int)timetask.TotalHours);
				return string.Format("{0} дн назад", (int)timetask.TotalDays);
			}
		}

		public string TimeTask2 {
			get {
				if (State == 3 || State == 2)
				{
					return string.Empty;
				}
				if (!ExecutionDate.HasValue)
					return string.Empty;
				if (DocumentType == 4)
					return string.Empty;
				TimeSpan timetask = (ExecutionDate.Value - DateTime.Now);
				if (timetask.TotalSeconds > 0) {
					if (timetask.TotalMinutes < 60)
						return string.Format("{0} мин", (int) timetask.TotalMinutes);
					if (timetask.TotalHours < 24)
						return string.Format("{0} ч", (int) timetask.TotalHours);
					return string.Format("{0} дн", (int) timetask.TotalDays);
				}
				timetask = -timetask;
				if (timetask.TotalMinutes < 60)
					return string.Format("-{0} мин", (int)timetask.TotalMinutes);
				if (timetask.TotalHours < 24)
					return string.Format("-{0} ч", (int)timetask.TotalHours);
				if (timetask.TotalDays < 31)
					return string.Format("-{0} дн", (int)timetask.TotalDays);
				return string.Format("-{0} мес", (int)timetask.TotalDays/30);
			}
		}

		public string ImageStateTitle {
			get {
				switch (State) {
					case 0:
						return "Новый";
					case 1:
						return "В работе";
					case 2:
						return "Исполненный положительно";
					case 3:
						return "Исполненный отрицательно";
					case 4:
						return "На исполнении";
				}
				return string.Empty;
			}
		}

		public string ImageTypeTitle {
			get {

				switch (Type) {
					case 0:
						return "На рассмотрении";
					case 1:
						return "Резолюция";
					case 2:
						return "Поручение";
					case 3:
						return TypeEx == 1 ? "Перевод" : "Согласование";
					case 4:
						return "На регистрацию";
					case 5:
						return "Уведомление";
				}
				return string.Empty;
			}
		}

		public string ImageMonitoringTypeTitle {
			get {

				switch (DocumentMonitoringType) {
					case 0:
						return "";
					case 1:
						return "Не контрольный";
					case 2:
						return "Контроль";
					case 3:
						return "Особоый контроль";
					case 4:
						return "До контроль";
				}
				return string.Empty;
			}
		}

		public int DocumentMonitoringType { get; set; }

		public bool IsAddBp { get; set; }

		private string _correspondentsValue;
		public string CorrespondentsValue {
			get {
				if (!string.IsNullOrEmpty(_correspondentsValue) && _correspondentsValue.Length > 150) {
					return _correspondentsValue.Substring(0, 150) + " ...";
				}
				return _correspondentsValue;
			}
			set { _correspondentsValue = value; }
		}


		public string OutgoingNumber { get; set; }

		public string ResponsibleValue { get; set; }
		public string DestinationValue { get; set; }
		public DateTime? OutgoingDate { get; set; }

		public int DocumentType { get; set; }
		public string OutgoingDateStr {
			get {
				if (OutgoingDate != null)
					return
						OutgoingDate.Value.ToShortDateString();
				return string.Empty;
			}
		}

        public string NameRu { get; set; }
        public string NameKz { get; set; }
        public string NameEn { get; set; }
        public string RegisterType { get; set; }
        public string StatusValue { get; set; }
        public string ProxyOrganizationName { get; set; }

    }
}