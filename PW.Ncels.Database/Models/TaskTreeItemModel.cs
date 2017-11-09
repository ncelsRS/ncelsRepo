using System;
using System.Collections.Generic;
using System.Linq;

namespace PW.Ncels.Database.Models {
	public class TaskTreeItemModel {

		public int Type { get; set; }

		public int TypeEx { get; set; }

		public int State { get; set; }

		public string CreateDateTime { get; set; }

		public string ExecutionDateTime { get; set; }

		private bool _expanded = true;

		public bool expanded {
			get { return _expanded; }
			set { _expanded = value; }
		}

		public bool IsConfirm { get; set; }
		
		public bool IsAllowEdit { get; set; }

		public Guid? TaskId { get; set; }

		public string Executor { get; set; }

		public string Text { get; set; }

		public string Number { get; set; }

		public bool IsMineLine { get; set; }

		public bool IsResponsible { get; set; }

		public bool IsReport {
			get { return Reports != null && Reports.Length > 0; }
		}

		public ReportTask[] Reports { get; set; }

		public string Image {
			get {
				if (Type > -1) {
					if (TypeEx == 1)
						return string.Format("../../Content/images/TaskType_31.png");
					return string.Format("../../Content/images/TaskType_{0}.png", Type);
				}
				return string.Format("../../Content/images/TaskStateType_{0}.png", State);
			}
		}

		public string ImageTitle {
			get {
				if (Type > -1) {
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
							return "на регистрацию";
						case 5:
							return "Уведомление";
					}
				}
				else {
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
				}
				return null;
			}
		}

		private IList<TaskTreeItemModel> _children;

		public IList<TaskTreeItemModel> Children {
			get {
				if (_children == null) {
					_children = new List<TaskTreeItemModel>();
				}
				return _children;
			}
			set { _children = value; }
		}

		public bool HasChildren {
			get { return Children.Any(); }
		}

		public Guid? Id { get; set; }
	}

	public class ReportTask {
		public string ReportDate { get; set; }

		public string ReportText { get; set; }
	}
}