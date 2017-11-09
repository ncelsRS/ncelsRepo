using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml;
using System.Xml.Linq;
using PW.Ncels.Database.DataModel;
using PW.Ncels.Database.Models;
using PW.Ncels.Database.Recources;


namespace PW.Ncels.Database.Helpers
{
    public class DictionaryHelper
    {
        public const string Separator = ", ";
        public static string GetItemsId(List<Item> items)
        {
            if (items == null || items.Count == 0)
                return string.Empty;
            return string.Join(", ", items.Select(o => o.Id).ToArray());
        }

        public static string GetItemId(Item item)
        {
            if (item == null)
                return string.Empty;
            return item.Id;
        }

        public static string GetItemsName(List<Item> items)
        {
            if (items == null || items.Count == 0)
                return string.Empty;
            return string.Join(", ", items.Select(o => o.Name).ToArray());
        }
        public static string GetItemName(Item item)
        {
            if (item == null)
                return string.Empty;
            return item.Name;
        }
        public static List<Item> GetItems(string itemId, string itemValue)
        {
            if (string.IsNullOrEmpty(itemId))
            {
                return new List<Item>();
            }
            string[] itemsId = itemId.Split(new[] { Separator }, StringSplitOptions.RemoveEmptyEntries);

            string[] itemsValue = itemsId.Length == 1 ? new string[] { itemValue } : itemValue.Split(new[] { Separator }, StringSplitOptions.RemoveEmptyEntries);
            if (itemsId.Length != itemsValue.Length)
            {
                return new List<Item>();
            }

            List<Item> items = new List<Item>();
            for (int i = 0; i < itemsId.Length; i++)
            {
                items.Add(new Item() { Id = itemsId[i], Name = itemsValue[i] });
            }
            return items;
        }

        public static List<DictionaryInfo> GetList()
        {


            XmlReader reader = XmlReader.Create(HttpContext.Current.Server.MapPath(@"..\Content\Xml\Dictionaries.xml"));
            XDocument doc = XDocument.Load(reader);
            XElement xElement = doc.Element("Dictionaries");
            if (xElement == null)
                return new List<DictionaryInfo>();
            return xElement.
                Elements().Select(o => new DictionaryInfo
                {
                    NameRu = o.Attribute("Name").Value,
                    TypeRu = o.Attribute("Type").Value,
                    NameKz = o.Attribute("NameKz").Value,
                    TypeKz = o.Attribute("Type").Value,
                    Description = o.Attribute("Description").Value
                }).OrderBy(o => o.Name).ToList();
        }

        public static List<DictionaryInfo> GetOBKList()
        {


            XmlReader reader = XmlReader.Create(HttpContext.Current.Server.MapPath(@"..\Content\Xml\OBKDictionaries.xml"));
            XDocument doc = XDocument.Load(reader);
            XElement xElement = doc.Element("Dictionaries");
            if (xElement == null)
                return new List<DictionaryInfo>();
            return xElement.
                Elements().Select(o => new DictionaryInfo
                {
                    NameRu = o.Attribute("Name").Value,
                    TypeRu = o.Attribute("Type").Value,
                    NameKz = o.Attribute("NameKz").Value,
                    TypeKz = o.Attribute("Type").Value,
                    Description = o.Attribute("Description").Value
                }).OrderBy(o => o.Name).ToList();
        }

        public static List<DictionaryInfo> GetNomenclatureList()
        {

            ncelsEntities entities = UserHelper.GetCn();
            var items = entities.Dictionaries.Where(o => o.Type == "Nomenclature").GroupBy(o => o.Year);
            return items.Select(o => new DictionaryInfo()
            {
                NameRu = o.Key,
                TypeRu = o.Key,
                NameKz = o.Key,
                TypeKz = o.Key
            }).OrderBy(o => o.NameRu).ToList();
        }

        public static List<DictionaryInfo> GetCorRefTypeList()
        {

            ncelsEntities entities = UserHelper.GetCn();
            var items = entities.Dictionaries.Where(o => o.Type == "DepartmentTypeDictionary").ToList();
            var data = items.Select(o => new DictionaryInfo()
            {
                NameRu = o.Name,
                NameKz = o.NameKz,
                TypeRu = o.Id.ToString(),
                TypeKz = o.Id.ToString(),
            }).OrderBy(o => o.NameRu).ToList();
            data.Insert(0, new DictionaryInfo() { NameRu = Messages.Property_Все_394__00, NameKz = Messages.Property_Все_394__00, TypeKz = null, TypeRu = null });
            return data;
        }

        public static Guid GetDictionaryIdFirst(string type)
        {
            ncelsEntities entities = UserHelper.GetCn();
            var dic = entities.Dictionaries.FirstOrDefault(m => m.Type == type);
            return dic?.Id ?? Guid.Empty;
        }

        public static int GetType(ncelsEntities db, Guid id)
        {
            if (db.PriceProjects.Any(m => m.Id == id))
            {
                return 1;
            }
            else if (db.RegisterProjects.Any(m => m.Id == id))
            {
                return 2;
            }
            else
            {
                return 3;
            }
        }

        public static Guid GetDicIdByCode(string type, string code)
        {
            ncelsEntities db = UserHelper.GetCn();
            var dic = db.Dictionaries.FirstOrDefault(m => m.Type == type && m.Code == code);
            if (dic != null)
            {
                return dic.Id;
            }
            return Guid.Empty;
        }

        public static Dictionary GetDicItemByCode(string type, string code)
        {
            ncelsEntities db = UserHelper.GetCn();
            var dic = db.Dictionaries.AsNoTracking().FirstOrDefault(m => m.Type == type && m.Code == code);
            return dic;
        }
        public static Dictionary GetDicItemById(Guid id)
        {
            ncelsEntities db = UserHelper.GetCn();
            return db.Dictionaries.AsNoTracking().FirstOrDefault(m => m.Id == id);
        }
    }
}