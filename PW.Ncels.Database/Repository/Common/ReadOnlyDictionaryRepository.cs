using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PW.Ncels.Database.DataModel;
using PW.Ncels.Database.Models.Expertise;
using PW.Ncels.Database.Models.OBK;

namespace PW.Ncels.Database.Repository.Common
{
    /// <summary>
    /// Работа с данными, со справочниками которые не добавляются через систему
    /// </summary>
    public class ReadOnlyDictionaryRepository : ARepository
    {
        /// <summary>
        /// Список "Тип ускоренной процедуры"
        /// </summary>
        /// <returns></returns>
        public IEnumerable<EXP_DIC_AccelerationType> GetAccelerationTypes()
        {
            return AppContext.EXP_DIC_AccelerationType.Where(e => !e.IsDeleted).OrderBy(e => e.Id);
        }
        /// <summary>
        /// Список "Лекарственное средство "
        /// </summary>
        /// <returns></returns>
        public IEnumerable<EXP_DIC_DrugType> GetDrugType()
        {
            return AppContext.EXP_DIC_DrugType.Where(e => !e.IsDeleted).OrderBy(e => e.Id);
        }
        /// <summary>
        /// Список "Лекарственное средство Вид"
        /// </summary>
        /// <returns></returns>
        public IEnumerable<EXP_DIC_DrugTypeKind> GetDrugTypeKinds()
        {
            return AppContext.EXP_DIC_DrugTypeKind.Where(e => !e.IsDeleted).OrderBy(e => e.Id);
        }
        /// <summary>
        /// Список "Форма отпуска в стране заявителя"
        /// </summary>
        /// <returns></returns>
        public IEnumerable<EXP_DIC_SaleType> GetSaleType()
        {
            return AppContext.EXP_DIC_SaleType.Where(e => !e.IsDeleted).OrderBy(e => e.Id);
        }

        /// <summary>
        /// Список "Вид упаковки"
        /// </summary>
        /// <returns></returns>
        public IEnumerable<EXP_DIC_WrappingType> GetWrappingTypes()
        {
            return AppContext.EXP_DIC_WrappingType.Where(e => !e.IsDeleted).OrderBy(e => e.Id);
        }

        /// <summary>
        /// Список "Происхождение"
        /// </summary>
        /// <returns></returns>
        public IEnumerable<EXP_DIC_Origin> GetOrigins()
        {
            return AppContext.EXP_DIC_Origin.Where(e => !e.IsDeleted).OrderBy(e => e.Id);
        }

        /// <summary>
        /// Список "Вид растения"
        /// </summary>
        /// <returns></returns>
        public IEnumerable<EXP_DIC_PlantKind> GetPlantKinds()
        {
            return AppContext.EXP_DIC_PlantKind.Where(e => !e.IsDeleted).OrderBy(e => e.Id);
        }

        public List<BoooleanEntity> GetBooleanList()
        {
            var list = new List<BoooleanEntity>();
            list.Add(new BoooleanEntity { IsSign = false, NameRu = "Нет" });
            list.Add(new BoooleanEntity { IsSign = true, NameRu = "Да" });
            return list;
        }

        public List<BoolenGMPCheck> GetCertificateGMPCheck()
        {
            var list = new List<BoolenGMPCheck>();
            list.Add(new BoolenGMPCheck { CertificateGMPCheck = false, NameRu = "Нет" });
            list.Add(new BoolenGMPCheck { CertificateGMPCheck = true, NameRu = "Да" });
            return list;
        }

        public List<OBK_Ref_Result> GetUOBKCheck()
        {
            var list = new List<OBK_Ref_Result>();
            list.Add(new OBK_Ref_Result { ExpertiseResult = true, Name = "Соответствует требованиям"});
            list.Add(new OBK_Ref_Result { ExpertiseResult = false, Name = "Не соответствует требованиям" });
            return list;
        }

        /// <summary>
        /// Список "Производство"
        /// </summary>
        /// <returns></returns>
        public IEnumerable<EXP_DIC_ManufactureType> GetManufactureTypeList()
        {
            return AppContext.EXP_DIC_ManufactureType.Where(e => !e.IsDeleted).OrderBy(e => e.Id);
        }

        /// <summary>
        /// Список "Из общего справочника"
        /// </summary>
        /// <returns></returns>
        public List<Dictionary> GetDictionaries(string type)
        {
            return AppContext.Dictionaries.Where(e => e.Type == type).ToList();
        }

        /// <summary>
        /// Значение "Из общего справочника"
        /// </summary>
        /// <returns></returns>
        public Dictionary GetDictionaryById(string type, Guid? id)
        {
            return AppContext.Dictionaries.FirstOrDefault(e => e.Type == type && e.Id == id);
        }

        /// <summary>
        /// Список "Тип замечании"
        /// </summary>
        /// <returns></returns>
        public IEnumerable<EXP_DIC_RemarkType> GetRemarkTypes()
        {
            return AppContext.EXP_DIC_RemarkType.Where(e => !e.IsDeleted).OrderBy(e => e.Id);
        }

        /// <summary>
        /// Список "Тип заявление"
        /// </summary>
        /// <returns></returns>
        public IEnumerable<EXP_DIC_Type> GetDicTypes()
        {
            return AppContext.EXP_DIC_Type.Where(e => !e.IsDeleted).OrderBy(e => e.Id);
        }

        /// <summary>
        /// Список "Тип изменение"
        /// </summary>
        /// <returns></returns>
        public IEnumerable<EXP_DIC_ChangeType> GetDicChangeTypes()
        {
            return AppContext.EXP_DIC_ChangeType.Where(e => !e.IsDeleted).OrderBy(e => e.Id);
        }
        /// <summary>
        ///  "Тип изменение"
        /// </summary>
        /// <returns></returns>
        public EXP_DIC_ChangeType GetDicChangeTypeById(int? id)
        {
            return AppContext.EXP_DIC_ChangeType.FirstOrDefault(e => e.Id == id);
        }

        /// <summary>
        /// Список "Тип НД"
        /// </summary>
        /// <returns></returns>
        public IEnumerable<EXP_DIC_TypeND> GetDicTypeNDs()
        {
            return AppContext.EXP_DIC_TypeND.Where(e => !e.IsDeleted).OrderBy(e => e.Id);
        }
        /// <summary>
        /// Список "Тип НД"
        /// </summary>
        /// <returns></returns>
        public IEnumerable<EXP_DIC_TypeFileND> GetDicTypeFileNDs()
        {
            return AppContext.EXP_DIC_TypeFileND.Where(e => !e.IsDeleted).OrderBy(e => e.Id);
        }

        /// <summary>
        /// Список "Нормативный Документ, регламентирующий качество или Фармакопея с указанием года издания"
        /// </summary>  
        /// <returns></returns>
        public IEnumerable<EXP_DIC_NormDocFarm> GetExpDicNormDocFarms()
        {
            return AppContext.EXP_DIC_NormDocFarm.Where(e => !e.IsDeleted).OrderBy(e => e.Id);
        }
        /// <summary>
        /// Список "ОТД"
        /// </summary>  
        /// <returns></returns>
        public IEnumerable<EXP_DIC_PrimaryOTD> GetExpDicPrimaryOTDs(string[] excludeModuleCodes = null)
        {
            var query = AppContext.EXP_DIC_PrimaryOTD.Where(e => !e.IsDeleted);
            if (excludeModuleCodes != null && excludeModuleCodes.Length > 0)
            {
                query = query.Where(e => !excludeModuleCodes.Contains(e.Code));
            }
            return query.OrderBy(e => e.Id);
        }
        public IEnumerable<EXP_DIC_PrimaryOTD> GetExpDicPrimaryOTDs(int? parent, string[] excludeModuleCodes = null)
        {
            var result = new List<EXP_DIC_PrimaryOTD>();
            var query = AppContext.EXP_DIC_PrimaryOTD.Where(e => !e.IsDeleted && e.ParentId == parent);
            if (excludeModuleCodes != null && excludeModuleCodes.Length > 0)
            {
                query = query.Where(e => !excludeModuleCodes.Contains(e.Code));
            }
            var items = query.OrderBy(e => e.Id).ToList();
            if (items.Count > 0)
            {
                foreach (var item in items)
                {
                    result.Add(item);
                    result.AddRange(GetExpDicPrimaryOTDs(item.Id));
                }
            }
            else
            {
                result.AddRange(result);
            }
            return result;
        }

        public IEnumerable<EXP_DIC_PrimaryFinalyDocResult> GetExpDicPrimaryFinalyDocResults()
        {
            return AppContext.EXP_DIC_PrimaryFinalyDocResult.Where(e => !e.IsDeleted).OrderBy(e => e.Id);
        }

        public IEnumerable<EXP_DIC_StageResult> GetStageResults()
        {
            return AppContext.EXP_DIC_StageResult.AsNoTracking().Where(e => !e.IsDeleted).ToList();
        }
        public IEnumerable<EXP_DIC_StageResult> GetStageResultsByStage(int stageId)
        {
            return AppContext.EXP_DIC_Stage.AsNoTracking().Where(e => !e.IsDeleted && e.Id == stageId).SelectMany(e => e.EXP_DIC_StageResults).ToList();
        }

        public EXP_DIC_StageResult GetStageResultById(int id)
        {
            return AppContext.EXP_DIC_StageResult.AsNoTracking().FirstOrDefault(sr => sr.Id == id);
        }

        public IEnumerable<EXP_DIC_Stage> GetDicStages()
        {
            return AppContext.EXP_DIC_Stage.AsNoTracking().Where(e => !e.IsDeleted).ToList();
        }

        public EXP_DIC_Stage GetDicStageById(int id)
        {
            return AppContext.EXP_DIC_Stage.FirstOrDefault(e => e.Id == id);
        }

        public IEnumerable<EXP_DIC_AnalyseIndicator> GetDicAnalyseIndicators()
        {
            return AppContext.EXP_DIC_AnalyseIndicator.Where(e => !e.IsDeleted);
        }

        public EXP_DIC_AnalyseIndicator GetDicAnalyseIndicatorById(int id)
        {
            return AppContext.EXP_DIC_AnalyseIndicator.FirstOrDefault(e => e.Id == id);
        }

        public DIC_FileLinkStatus GetDicFileLinkStatusByCode(string code)
        {
            return AppContext.DIC_FileLinkStatus.FirstOrDefault(e => e.Code == code);

        }

        public EXP_DIC_Status GetDicStatusById(int id)
        {
            return AppContext.EXP_DIC_Status.FirstOrDefault(e => e.Id == id);
        }
    }
}
