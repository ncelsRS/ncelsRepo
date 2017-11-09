using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;

namespace PW.Ncels.Database.Helpers{

    public static class EnumHelper{
        /// <summary>
        /// Возвращает описание из DescriptionAttribute примененного к элементу перечисления
        /// </summary>
        /// <param name="enumObj">Перечисление</param>
        /// <returns>Description</returns>
        public static string GetDescription(this Enum enumObj){
            var fieldInfo = enumObj.GetType().GetField(enumObj.ToString());

            var attribArray = fieldInfo.GetCustomAttributes(false);

            if (attribArray.Length == 0){
                return enumObj.ToString();
            }
            var attrib = attribArray[0] as DescriptionAttribute;
            return attrib != null ? attrib.Description : enumObj.ToString();
        }

        /// <summary>
        /// Возвращает все значения элемента перечисления
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static IEnumerable<T> GetValues<T>(){
            return Enum.GetValues(typeof(T)).Cast<T>();
        }

        /// <summary>
        /// Возвращает DisplayName из ресурсов по указанному значению Перечисления
        /// </summary>
        /// <param name="obj">Значение Enum </param>
        /// <returns>Значние, иначе NULL</returns>
        public static string GetDisplayName(Enum obj){
            var type = obj.GetType();
            var fields = type.GetFields();

            foreach (var field in fields){
                var value = field.GetValue(obj);
                if (value.Equals(obj)){
                    var attrs = field.GetCustomAttributes(typeof(DisplayAttribute), true);
                    if (attrs != null && attrs.Length > 0)
                        if (attrs[0] is DisplayAttribute)
                            return ((DisplayAttribute)attrs[0]).Name;
                }
            }
            return null;
        }


        /// <summary>
        /// Возвращает DisplayName из ресурсов по указанному значению Перечисления
        /// </summary>
        /// <param name="obj">Значение Enum </param>
        /// <returns>Значние, иначе NULL</returns>
        public static string DisplayName(this Enum obj){
            return GetDisplayName(obj);
        }

        /// <summary>
        /// Возвращает коллекцию "Значение" - "Имя для отображения" указанного перечисления 
        /// </summary>
        /// <returns>Возвращает коллекцию ключ значния, Гарантировано не null</returns>
        public static IList<KeyValuePair<int,string>> GetDisplayNameEnumList<TEnum>() {
			var result = new List<KeyValuePair<int, string>>();
			if (typeof (TEnum).IsEnum) {
				var fields = typeof (TEnum).GetFields();
				for (int i=1; i < fields.Length; i++) {
					var value = (int)fields[i].GetValue(null);
					string displayName = null;
					var attrs = fields[i].GetCustomAttributes(true);
					if (attrs != null && attrs.Length > 0)
						if (attrs[0] is DisplayAttribute)
							displayName = ((DisplayAttribute)attrs[0]).Name;
					if (!string.IsNullOrEmpty(displayName))
						result.Add(new KeyValuePair<int, string>(value, displayName));
				}
			}
			return result;
		}

		/// <summary>
		/// Возвращает первый попавшийся атрибут
		/// </summary>
		/// <param name="type">Тип</param>
		/// <returns>Если нет атрибута то null</returns>
		public static DisplayAttribute GetAttribute(Type type) {
			var attrs = type.GetCustomAttributes(true);
			foreach (var attr in attrs) {
				if (attr is DisplayAttribute)
					return attr as DisplayAttribute;
			}
			return null;
		}

    }
   
}