using System.Reflection;
using System.Resources;
using System.Web;
using Aspose.Pdf;
using PW.Ncels.Database.Recources;


namespace PW.Ncels.Database.DataModel {
	public partial class History {
		public string ColumnNameStr {
			get {
				if (!string.IsNullOrEmpty(ColumnName)) {
					ResourceManager rm = new ResourceManager(typeof(HistoryStr));
					string str = rm.GetString(ColumnName);
					if (!string.IsNullOrEmpty(str)) {
						return rm.GetString(ColumnName);
					}
				}
				return ColumnName;
			}
		}

		public string OperationStr {
			get {
				if (!string.IsNullOrEmpty(OperationId)) {
					ResourceManager rm = new ResourceManager(typeof(HistoryStr));
					return rm.GetString(OperationId);
				}
				return string.Empty;
			}
		}

		public string TableNameStr {
			get {
				if (!string.IsNullOrEmpty(TableName)) {
					ResourceManager rm = new ResourceManager(typeof(HistoryStr));
					return rm.GetString(TableName);
				}
				return string.Empty;
			}
		}
	}
}