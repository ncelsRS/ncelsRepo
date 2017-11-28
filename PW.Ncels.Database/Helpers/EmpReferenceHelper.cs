using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PW.Ncels.Database.DataModel;

namespace PW.Ncels.Database.Helpers
{
    public class EmpReferenceHelper
    {
        /// <summary>
        /// Справочник банков, возвращает даже не подвержденные
        /// </summary>
        /// <returns></returns>
        public static List<EMP_Ref_Bank> GetBanks()
        {
            ncelsEntities entities = UserHelper.GetCn();
            var banks = entities.EMP_Ref_Bank.Where(e => !e.IsDeleted).ToList();
            return banks;
        }

        public static List<EMP_Ref_ServiceType> GetServiceType()
        {
            ncelsEntities entities = UserHelper.GetCn();
            var serviceType = entities.EMP_Ref_ServiceType.ToList();
            return serviceType;
        }

        public static List<EMP_Ref_ServiceType> GetServiceTypeParentId(Guid id)
        {
            ncelsEntities entities = UserHelper.GetCn();
            var serviceType = entities.EMP_Ref_ServiceType.Where(e=>e.ParentId == id).ToList();
            return serviceType;
        }
        public static List<EMP_Ref_PriceList> GetPriceList(Guid id)
        {
            ncelsEntities entities = UserHelper.GetCn();
            var priceList = entities.EMP_Ref_PriceList.Where(e => e.ServiceTypeId == id).ToList();
            return priceList;
        }

        public static List<EMP_Ref_PriceType> GetPriceType(IEnumerable<Guid> priceLists)
        {
            ncelsEntities entities = UserHelper.GetCn();
            var priceType = entities.EMP_Ref_PriceType.Where(e=> priceLists.Contains(e.Id)).ToList();
            return priceType;
        }
    }
}
