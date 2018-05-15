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
        [HttpGet]
        public async Task<IActionResult> StorageCondition()
        {
            return Ok(await _refLogic.GetStorageCondition());
        }

        /// <summary>
        /// Классификатор областей медицинского применения медицинских изделий(справочник)
        /// </summary>
        [Route("ClassifierMedicalArea")]
        [HttpGet]
        public async Task<IActionResult> ClassifierMedicalArea()
        {
            return Ok(await _refLogic.GetClassifierMedicalArea());
        }

        /// <summary>
        /// Код Номенклатуры медицинских изделий Республики Казахстан(справочник)
        /// </summary>
        /// <param name="name">название издения</param>
        /// <param name="culture">култура(ru, kz)</param>
        /// <returns></returns>
        [Route("NomenclatureCodeMedProduct")]
        [HttpGet]
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
        [HttpGet]
        public IActionResult RegistrationType()
        {
            return Ok(_refLogic.GetRegistrationType());
        }

        /// <summary>
        /// Класс в зависимости от степени потенциального риска применения(справочник)
        /// </summary>
        /// <returns></returns>
        [Route("DegreeRiskClass")]
        [HttpGet]
        public async Task<IActionResult> DegreeRiskClass()
        {
            return Ok(await _refLogic.GetDegreeRiskClass());
        }

        /// <summary>
        /// Организационная форма(справочник)
        /// </summary>
        /// <returns></returns>
        [Route("OrganizationForm")]
        [HttpGet]
        public async Task<IActionResult> OrganizationForm()
        {
            return Ok(await _refLogic.GetOrganizationForm());
        }

        /// <summary>
        /// Добавить новую Организационную форума(справочник)
        /// </summary>
        /// <param name="nameRu">наименование на русском</param>
        /// <param name="nameKz">наимемнование на казахском</param>
        /// <returns></returns>
        [Route("SaveOrganizationForm")]
        [HttpPut]
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
        [HttpGet]
        public async Task<IActionResult> Bank()
        {
            return Ok(await _refLogic.GetBank());
        }

        /// <summary>
        /// Добавить новый Банк(справочник)
        /// </summary>
        /// <param name="nameRu">наименование на русском</param>
        /// <param name="nameKz">наимемнование на казахском</param>
        /// <returns></returns>
        [Route("SaveBank")]
        [HttpPut]
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
        [HttpGet]
        public async Task<IActionResult> Currency()
        {
            return Ok(await _refLogic.GetCurrency());
        }

        /// <summary>
        /// Страны(справочник)
        /// </summary>
        /// <returns></returns>
        [Route("Country")]
        [HttpGet]
        public async Task<IActionResult> Country()
        {
            return Ok(await _refLogic.GetCountry());
        }

        /// <summary>
        /// Договор составляется с
        /// </summary>
        /// <returns></returns>
        [Route("HolderType")]
        [HttpGet]
        public IEnumerable<object> HolderType()
        {
            return _refLogic.GetHolderType();
        }

        /// <summary>
        /// Тип договора
        /// </summary>
        /// <returns></returns>
        [Route("ContractForm")]
        [HttpGet]
        public IEnumerable<object> ContractForm()
        {
            return _refLogic.GetContractForm();
        }

        /// <summary>
        /// Плательщик
        /// </summary>
        /// <returns></returns>
        [Route("ChosenPayer")]
        [HttpGet]
        public IEnumerable<object> ChosenPayer()
        {
            return _refLogic.GetChosenPayer();
        }

        /// <summary>
        /// Тип заявки для калькулятора
        /// </summary>
        /// <param name="contractScope">Нац или ЕАЭС</param>
        /// <param name="contractForm">Регистрация, перерегистрация или внесение изменений</param>
        /// <returns></returns>
        [Route("CalculatorApplicationType")]
        [HttpGet]
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
        [HttpGet]
        public async Task<IActionResult> CalculatorServiceType(int applicationTypeId)
        {
            return Ok(await _refLogic.GetCalculatorServiceType(applicationTypeId));
        }

        /// <summary>
        /// Калькулятор результат
        /// </summary>
        /// <returns></returns>
        [Route("ShowPriceList")]
        [HttpGet]
        public async Task<IActionResult> ShowPriceList(bool isImport, int serviceTypeId, int? serviceTypeModifId)
        {
            return Ok(await _refLogic.GetShowPrice(isImport, serviceTypeId, serviceTypeModifId));
        }

        /// <summary>
        /// Тип комплектации ИМН и МТ
        /// </summary>
        /// <returns></returns>
        [Route("EquipmentType")]
        [HttpGet]
        public async Task<IActionResult> EquipmentType()
        {
            return Ok(await _refLogic.GetEquipmentType());
        }

        /// <summary>
        /// Тип упаковки
        /// </summary>
        /// <returns></returns>
        [Route("PackagingType")]
        [HttpGet]
        public async Task<IActionResult> PackagingType()
        {
            return Ok(await _refLogic.GetPackagingType());
        }

        /// <summary>
        /// Единица измерения
        /// </summary>
        /// <returns></returns>
        [Route("Measure")]
        [HttpGet]
        public async Task<IActionResult> Measure()
        {
            return Ok(await _refLogic.GetMeasure());
        }
    }
}
