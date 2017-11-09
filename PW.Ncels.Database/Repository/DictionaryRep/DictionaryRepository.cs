using System;
using System.Linq;

namespace PW.Ncels.Database.Repository.DictionaryRep
{
    public class DictionaryRepository : ARepositoryGeneric<DataModel.Dictionary>
    {
        public DictionaryRepository(bool isProxy = true):base(isProxy)
        { }
        
        public Guid? GetDictionaryElementIdByCode(string code)
        {
            return AppContext.Dictionaries.Where(d => d.Code == code).Select(d => d.Id).FirstOrDefault();
        }

        public Guid? GetDictionaryElementIdByTypeAndCode(string type, string code)
        {
            return AppContext.Dictionaries.Where(d => d.Type == type && d.Code == code).Select(d => d.Id).FirstOrDefault();
        }

        public Guid? GetDictionaryIdByTypeAndDisplayName(string type, string displayName)
        {
            return AppContext.Dictionaries.Where(d => d.Type == type && d.DisplayName == displayName).Select(d => d.Id).FirstOrDefault();
        }
    }
}