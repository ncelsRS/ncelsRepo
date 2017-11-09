using System;
using PW.Ncels.Database.Helpers;

namespace PW.Ncels.Database.Models {
	/// <summary>
	/// Информация о справочнике
	/// </summary>
	public class DictionaryInfo {
		public DictionaryInfo() {
			Id = Guid.NewGuid();
		}

		public Guid Id { get; set; }

		public string Name
		{
			get { return LocalizationHelper.GetString(NameRu, NameKz); }
		}

		public string Type
		{
			get { return LocalizationHelper.GetString(TypeRu, TypeKz); }
		}
		public string NameKz { get; set; }
		public string TypeKz { get; set; }

		public string NameRu { get; set; }
		public string TypeRu { get; set; }
		public string Description { get; set; }
		public string Report { get; set; }
	}
}