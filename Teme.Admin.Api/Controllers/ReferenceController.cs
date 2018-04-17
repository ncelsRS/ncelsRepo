using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Teme.Admin.Data.Model;
using Teme.Admin.Logic;

namespace Teme.Admin.Api.Controllers
{
    [Route("api/[controller]")]
    public class ReferenceController : Controller
    {
        private ReferenceLogic _refLogic { get; }

        public ReferenceController(ReferenceLogic refLogic)
        {
            _refLogic = refLogic;
        }

        /// <summary>
        /// Условия хранения(справочник)
        /// </summary>
        /// <returns></returns>
        [Route("StorageCondition")]
        public async Task<IEnumerable<ReferenceModel>> StorageCondition()
        {
            return await _refLogic.GetStorageCondition();
        }

        /// <summary>
        /// Классификатор областей медицинского применения медицинских изделий(справочник)
        /// </summary>
        [Route("ClassifierMedicalArea")]
        public IEnumerable<ReferenceModel> ClassifierMedicalArea()
        {
            return _refLogic.GetClassifierMedicalArea();
        }

        /// <summary>
        /// Код Номенклатуры медицинских изделий Республики Казахстан(справочник)
        /// </summary>
        /// <returns></returns>
        [Route("NomenclatureCodeMedProduct")]
        public IEnumerable<NomenclatureCodeMedProductModel> NomenclatureCodeMedProduct(string name, string culture)
        {
            return _refLogic.GetNomenclatureCodeMedProduct(name, culture);
        }

        /// <summary>
        /// Тип регистрации(справочник)
        /// </summary>
        /// <returns></returns>
        [Route("RegistrationType")]
        public IEnumerable<object> RegistrationType()
        {
            return _refLogic.GetRegistrationType();
        }

        /// <summary>
        /// Класс в зависимости от степени потенциального риска применения(справочник)
        /// </summary>
        /// <returns></returns>
        [Route("DegreeRiskClass")]
        public IEnumerable<object> DegreeRiskClass()
        {
            return _refLogic.GetDegreeRiskClass();
        }

        /// <summary>
        /// Организационная форма(справочник)
        /// </summary>
        /// <returns></returns>
        [Route("OrganizationForm")]
        public IEnumerable<object> OrganizationForm()
        {
            return _refLogic.GetOrganizationForm();
        }

        /// <summary>
        /// Добавить новую Организационную форума(справочник)
        /// </summary>
        /// <param name="nameRu"></param>
        /// <param name="nameKz"></param>
        /// <returns></returns>
        [Route("SaveOrganizationForm")]
        public int SaveOrganizationForm(string nameRu, string nameKz)
        {
            return _refLogic.SaveOrganizationForm(nameRu, nameKz);
        }

        /// <summary>
        /// Банки(справочник)
        /// </summary>
        /// <returns></returns>
        [Route("Bank")]
        public IEnumerable<object> Bank()
        {
            return _refLogic.GetBank();
        }

        /// <summary>
        /// Добавить новый Банк(справочник)
        /// </summary>
        /// <param name="nameRu"></param>
        /// <param name="nameKz"></param>
        /// <returns></returns>
        [Route("SaveOrganizationForm")]
        public int SaveBank(string nameRu, string nameKz)
        {
            return _refLogic.SaveBank(nameRu, nameKz);
        }

        /// <summary>
        /// Валюта(справочник)
        /// </summary>
        /// <returns></returns>
        [Route("Currency")]
        public IEnumerable<object> Currency()
        {
            return _refLogic.GetCurrency();
        }

        /// <summary>
        /// Страны(справочник)
        /// </summary>
        /// <returns></returns>
        [Route("Currency")]
        public IEnumerable<object> Country()
        {
            return _refLogic.GetCountry();
        }

        /// <summary>
        /// Договор составляется с
        /// </summary>
        /// <returns></returns>
        [Route("HolderType")]
        public IEnumerable<object> HolderType()
        {
            return _refLogic.GetHolderType();
        }

        /// <summary>
        /// Тип договора
        /// </summary>
        /// <returns></returns>
        [Route("ContractForm")]
        public IEnumerable<object> ContractForm()
        {
            return _refLogic.GetContractForm();
        }
    }
}
