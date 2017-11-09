using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PW.Ncels.Database.DataModel;
using PW.Ncels.Database.Models.Expertise;

namespace PW.Ncels.Database.Repository.Expertise
{
    /// <summary>
    /// Работа с данными из внешних источников
    /// </summary>
    public class ExternalRepository : ARepository
    {
        /// <summary>
        /// Поиск по МНН
        /// </summary>
        public List<TermSearch> SearchMnn(string term)
        {
            var query =
                AppContext.sr_international_names.Where(
                        e =>
                            !e.block_sign &&
                            (e.name_rus.Contains(term) || e.name_kz.Contains(term) || e.name_lat.Contains(term)))
                    .Select(x => new TermSearch() {Id = x.id.ToString(), Term = x.name_rus + "/"+x.name_lat+"/"+x.name_kz});
            return query.ToList();
        }

        /// <summary>
        /// Поиск по Гос. реестру
        /// </summary>
        public List<TermSearch> SearchReestr(string term, int type)
        {
            var query =
                AppContext.sr_register.Where(
                        e =>
                            e.reg_type_id== type &&
                            (e.reg_number.Contains(term) || e.reg_number_kz.Contains(term)))
                    .Select(x => new TermSearch() { Id = x.id.ToString(), Term = x.reg_number + "/" + x.reg_number_kz});
            return query.ToList();
        }
        /// <summary>
        /// Поиск по Лекарственная форма
        /// </summary>
        public List<TermSearch> SearchDrugForm(string term)
        {
            var query =
                AppContext.sr_drug_forms.Where(
                        e =>
                            (e.full_name.Contains(term)))
                    .Select(x => new TermSearch() { Id = x.id.ToString(), Term = x.full_name  });
            return query.ToList();
        }

        /// <summary>
        /// Поиск Лекарственная форма по id
        /// </summary>
        public sr_drug_forms GetDrugFormById(int id)
        {
            return AppContext.sr_drug_forms.FirstOrDefault(e => e.id == id);
        }
        /// <summary>
        /// Поиск МНН по id
        /// </summary>
        public sr_international_names GetMnnById(int id)
        {
            return AppContext.sr_international_names.FirstOrDefault(e => e.id == id);
        }
        /// <summary>
        /// Список стран
        /// </summary>
        /// <returns></returns>
        public IEnumerable<sr_countries> GetCounties()
        {
            return AppContext.sr_countries.Where(e => !e.block_sign);
        }
        /// <summary>
        /// Список АТХ
        /// </summary>
        public List<sr_atc_codes> GetAtcList()
        {
            return AppContext.sr_atc_codes.Where(e => !e.block_sign).ToList();
        }
        /// <summary>
        /// Список АТХ
        /// </summary>
        public List<sr_atc_codes> GetAtcListByParent(int? parentId)
        {
            return AppContext.sr_atc_codes.Where(e => !e.block_sign && e.parent_id== parentId).ToList();
        }
        /// <summary>
        /// Поиск АТХ по id
        /// </summary>
        public sr_atc_codes GeAtcById(int id)
        {
            return AppContext.sr_atc_codes.FirstOrDefault(e => e.id == id);
        }

        /// <summary>
        /// Список метод ввода
        /// </summary>
        public virtual List<sr_use_methods> GetUseMethods()
        {
            return AppContext.sr_use_methods.Where(e => !e.block_sign ).ToList();
        }

        /// <summary>
        /// Список ед. измерения
        /// </summary>
        public List<sr_measures> GetMeasures()
        {
            return AppContext.sr_measures.Where(e => !e.block_sign && e.id>0).ToList();
        }

        /// <summary>
        /// Список упаковки
        /// </summary>
        public List<sr_boxes> GetBoxes()
        {
            return AppContext.sr_boxes.Where(e => !e.block_sign).ToList();
        }

        public List<TermSearch> SelectSubstance(string term)
        {
            var query =
                AppContext.sr_substances.Where(
                        e =>
                            (e.name.Contains(term)))
                    .Select(x => new TermSearch() { Id = x.id.ToString(), Term = x.name });
            return query.ToList();
        }

        public sr_substances GetSubstanceById(int id)
        {
            return AppContext.sr_substances.FirstOrDefault(e => e.id == id);
        }

        /// <summary>
        /// Список АТХ
        /// </summary>
        public List<sr_substance_types> GetSubstanceTypes()
        {
            return AppContext.sr_substance_types.ToList();
        }

        public sr_register_drugs GEtRegisterDrugById(int? id)
        {
            return AppContext.sr_register_drugs.FirstOrDefault(e => e.id == id);
        }
        /// <summary>
        /// Поиск Гос. реестр по id
        /// </summary>
        public sr_register GetReestrById(int id)
        {
            return AppContext.sr_register.FirstOrDefault(e => e.id == id);
        }

        /// <summary>
        /// Поиск по Лекарственная форма
        /// </summary>
        public List<TermSearch> SearchDosageForm(string term)
        {
            var query =
                AppContext.sr_dosage_forms.Where(
                        e => !e.block_sign &&
                            (e.name.Contains(term) || e.name_kz.Contains(term)))
                    .Select(x => new TermSearch() { Id = x.id.ToString(), Term = x.name +"/" + x.name_kz });
            return query.ToList();
        }

        /// <summary>
        /// Поиск Лекарственная форма по id
        /// </summary>
        public sr_dosage_forms GetDosageFormById(int id)
        {
            return AppContext.sr_dosage_forms.FirstOrDefault(e => e.id == id);
        }
    }
}
