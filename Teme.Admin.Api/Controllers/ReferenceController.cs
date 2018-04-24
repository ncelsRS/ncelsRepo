using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
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
        public async Task<IEnumerable<ReferenceModel>> ClassifierMedicalArea()
        {
            return await _refLogic.GetClassifierMedicalArea();
        }

        /// <summary>
        /// Код Номенклатуры медицинских изделий Республики Казахстан(справочник)
        /// </summary>
        /// <param name="name">название издения</param>
        /// <param name="culture">култура(ru, kz)</param>
        /// <returns></returns>
        [Route("NomenclatureCodeMedProduct")]
        public async Task<IActionResult> NomenclatureCodeMedProduct(string name, string culture)
        {
            if (name == null || culture == null)
                return BadRequest();
            return Ok(await _refLogic.GetNomenclatureCodeMedProduct(name, culture));
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
        public async Task<IEnumerable<object>> DegreeRiskClass()
        {
            return await _refLogic.GetDegreeRiskClass();
        }

        /// <summary>
        /// Организационная форма(справочник)
        /// </summary>
        /// <returns></returns>
        [Route("OrganizationForm")]
        public async Task<IEnumerable<object>> OrganizationForm()
        {
            return await _refLogic.GetOrganizationForm();
        }

        /// <summary>
        /// Добавить новую Организационную форума(справочник)
        /// </summary>
        /// <param name="nameRu">наименование на русском</param>
        /// <param name="nameKz">наимемнование на казахском</param>
        /// <returns></returns>
        [Route("SaveOrganizationForm")]
        public async Task<IActionResult> SaveOrganizationForm(string nameRu, string nameKz)
        {
            if (nameRu == null || nameKz == null)
                return BadRequest();
            return Ok(await _refLogic.SaveOrganizationForm(nameRu, nameKz));
        }

        /// <summary>
        /// Банки(справочник)
        /// </summary>
        /// <returns></returns>
        [Route("Bank")]
        public async Task<IEnumerable<object>> Bank()
        {
            return await _refLogic.GetBank();
        }

        /// <summary>
        /// Добавить новый Банк(справочник)
        /// </summary>
        /// <param name="nameRu">наименование на русском</param>
        /// <param name="nameKz">наимемнование на казахском</param>
        /// <returns></returns>
        [Route("SaveBank")]
        public async Task<IActionResult> SaveBank(string nameRu, string nameKz)
        {
            if (nameRu == null || nameKz == null)
                return BadRequest();
            return Ok(await _refLogic.SaveBank(nameRu, nameKz));
        }

        /// <summary>
        /// Валюта(справочник)
        /// </summary>
        /// <returns></returns>
        [Route("Currency")]
        public async Task<IEnumerable<object>> Currency()
        {
            return await _refLogic.GetCurrency();
        }

        /// <summary>
        /// Страны(справочник)
        /// </summary>
        /// <returns></returns>
        [Route("Country")]
        public async Task<IEnumerable<object>> Country()
        {
            return await _refLogic.GetCountry();
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

        /// <summary>
        /// Тип заявки для калькулятора
        /// </summary>
        /// <param name="contractScope">Нац или ЕАЭС</param>
        /// <param name="contractForm">Регистрация, перерегистрация или внесение изменений</param>
        /// <returns></returns>
        [Route("CalculatorApplicationType")]
        public async Task<IActionResult> CalculatorApplicationType(string contractScope, int contractForm)
        {
            if (contractScope == null)
                return BadRequest();
            return Ok(await _refLogic.GetCalculatorApplicationType(contractScope, contractForm));
        }

        /// <summary>
        /// Тип услуги для калькулятора
        /// </summary>
        [Route("CalculatorServiceType")]
        public async Task<IActionResult> CalculatorServiceType(int applicationTypeId)
        {
            return Ok(await _refLogic.GetCalculatorServiceType(applicationTypeId));
        }

        /// <summary>
        /// Калькулятор результат
        /// </summary>
        /// <returns></returns>
        [Route("ShowPriceList")]
        public async Task<IActionResult> ShowPriceList(bool isImport, int serviceTypeId, int? serviceTypeModifId)
        {
            return Ok(await _refLogic.GetShowPrice(isImport, serviceTypeId, serviceTypeModifId));
        }
    }
}
