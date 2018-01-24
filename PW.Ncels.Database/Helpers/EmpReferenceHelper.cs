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
            NcelsEntities entities = UserHelper.GetCn();
            var banks = entities.EMP_Ref_Bank.Where(e => !e.IsDeleted).ToList();
            return banks;
        }
        /// <summary>
        /// Справочник тип изменения(договора)
        /// </summary>
        /// <returns></returns>
        public static List<EMP_Ref_ChangeType> GetChangeType()
        {
            NcelsEntities entities = UserHelper.GetCn();
            var changeType = entities.EMP_Ref_ChangeType.ToList();
            return changeType;
        }
        /// <summary>
        /// Справочник тип заявки(договора)
        /// </summary>
        /// <returns></returns>
        public static List<EMP_Ref_ServiceType> GetServiceType()
        {
            NcelsEntities entities = UserHelper.GetCn();
            var serviceType = entities.EMP_Ref_ServiceType.ToList();
            return serviceType;
        }
        /// <summary>
        /// Справочник тип услуги(заявки по ParentId)(договора)
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static List<EMP_Ref_ServiceType> GetServiceTypeParentId(Guid id)
        {
            NcelsEntities entities = UserHelper.GetCn();
            var serviceType = entities.EMP_Ref_ServiceType.Where(e=>e.ParentId == id && !e.IsDeleted).ToList();
            return serviceType;
        }
        /// <summary>
        /// Справчоник прайс лист калькулятор (договора)
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static List<EMP_Ref_PriceList> GetPriceList(Guid id)
        {
            NcelsEntities entities = UserHelper.GetCn();
            var priceList = entities.EMP_Ref_PriceList.Where(e => e.ServiceTypeId == id).ToList();
            return priceList;
        }
        /// <summary>
        /// Справочник единиц измерений(догвора)
        /// </summary>
        /// <returns></returns>
        public static List<EMP_Ref_PriceType> GetPriceType()
        {
            NcelsEntities entities = UserHelper.GetCn();
            var priceType = entities.EMP_Ref_PriceType.ToList();
            return priceType;
        }
        /// <summary>
        /// Справочник единиц измерений(догвора)
        /// </summary>
        /// <returns></returns>
        public static List<EMP_Ref_PriceType> GetPriceType(IEnumerable<Guid> priceLists)
        {
            NcelsEntities entities = UserHelper.GetCn();
            var priceType = entities.EMP_Ref_PriceType.Where(e=> priceLists.Contains(e.Id)).ToList();
            return priceType;
        }
    }
}
