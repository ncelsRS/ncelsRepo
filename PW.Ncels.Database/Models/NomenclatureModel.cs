using System;
using System.Collections.Generic;
using PW.Ncels.Database.Helpers;

namespace PW.Ncels.Database.Models
{
	public class NomenclatureModel
	{
		public Guid? Id { get; set; }
		public string Name { get; set; }
		public string NameKz { get; set; }
		public string Code { get; set; }
		public string Type { get; set; }
		public string Note { get; set; }
		public List<Item> DepartmentsId { get; set; }

		public string DepartmentsValue {
			get { return DictionaryHelper.GetItemsName(DepartmentsId); }
		}

		public int Index {
			get {
				string[] array = Code.Split('–','-', '.');

				int index = 0;

				if (array.Length > 1) {
                
					index += int.Parse(array[0])*10000;
					if (array.Length == 3) {
						index += int.Parse(array[1])*100;
						index += int.Parse(array[2])*1;
					}
					else
						index += int.Parse(array[1])*1;
				}
				return index;
			}
		}
	}
}