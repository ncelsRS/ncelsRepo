﻿using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Teme.Contract.Data;
using Teme.Contract.Data.DTO;
using Teme.Contract.Data.Model;
using Teme.Contract.Infrastructure;
using Teme.Contract.Infrastructure.Primitives;
using Teme.Contract.Infrastructure.Workflow;
using Teme.Shared.Data.Context;
using Teme.Shared.Data.Primitives.Contract;
using Teme.Shared.Data.Repos.ContractRepo;
using Teme.Shared.Logic.ContractLogic;

namespace Teme.Contract.Logic
{
    public class ContractLogic : BaseContractLogic<IContractRepo>, IContractLogic
    {
        private readonly IContractWorkflowLogic _wflogic;
        private readonly IContractRepo _repo;
        private readonly IContractStatePolicyLogic _contractSp;
        private readonly IConvertDtoRepo _dtoRepo;

        public ContractLogic(IContractRepo repo, IContractWorkflowLogic wflogic, IContractStatePolicyLogic contractSp, IConvertDtoRepo dtoRepo) : base(repo)
        {
            _wflogic = wflogic;
            _contractSp = contractSp;
            _repo = repo;
            _dtoRepo = dtoRepo;
        }

        /// <summary>
        /// Создание договора
        /// </summary>
        /// <returns></returns>
        public async Task<object> Create(ContractTypeEnum contractType, string contractScope)
        {
            var workflowId = await _wflogic.Create();
            Shared.Data.Context.Contract contract = new Shared.Data.Context.Contract()
            {
                WorkflowId = workflowId.GetType().GetProperty("workflowId").GetValue(workflowId).ToString(),
                ContractType = contractType,
                ContractScope = contractScope
            };
            await _repo.CreateContract(contract);
            await _repo.SaveStatePolice(_contractSp.GetStatePolicy("DeclarantCreateContract", contract.Id), contract.Id);
            return new {
                contract.Id,
                contract.WorkflowId,
                ClassNames = new Dictionary<string, string> {
                    { "Contract", "Teme.Shared.Data.Context.Contract" },
                    { "Declarant", "Teme.Shared.Data.Context.Declarant" },
                    { "DeclarantDetail", "Teme.Shared.Data.Context.DeclarantDetail" },
                    { "CostWork", "Teme.Shared.Data.Context.CostWork" }
                }
            };
        }

        /// <summary>
        /// Поиск Заявителя резидента
        /// </summary>
        /// <param name="iin">ИИН или БИН заявиетля</param>
        /// <returns></returns>
        public async Task<object> SearchDeclarantResident(string iin)
        {
            var declarant = await _repo.SearchDeclarantResident(iin);
            if(declarant != null)
                return _dtoRepo.ConvertEntityToDeclarant(declarant);
            return null;
        }

        /// <summary>
        /// Поиск заявителя не резидента
        /// </summary>
        /// <param name="countryId">Id страны</param>
        /// <returns></returns>
        public async Task<object> SearchDeclarantNonResident(int countryId)
        {
            var declarant = await _repo.SearchDeclarantNonResident(countryId);
            if (declarant.Any())
                return declarant.Select(e=> new { e.Id, e.NameRu, e.NameKz, e.NameEn });
            return null;
        }

        /// <summary>
        /// Получение заявителя по Id
        /// </summary>
        /// <param name="id">Id заявителя</param>
        /// <returns></returns>
        public async Task<object> GetDeclarantById(int id)
        {
            var declarant = await _repo.GetDeclarant(id);
            if (declarant != null)
                return _dtoRepo.ConvertEntityToDeclarant(declarant);
            return null;
        }

        /// <summary>
        /// Добавления заявтеля по типам
        /// </summary>
        /// <param name="contractId">Id договра</param>
        /// <param name="code">Коды: Declarant, Manufactur, Payer</param>
        /// <returns></returns>
        public async Task<object> AddDeclarant(int contractId, string code)
        {
            var declarant = new Shared.Data.Context.Declarant();
            await _repo.CreateDeclarant(declarant);
            var declarantDetail = new Shared.Data.Context.DeclarantDetail()
            {
                DeclarantId = declarant.Id
            };
            await _repo.CreateDeclarantDetail(declarantDetail);
            var contract = await _repo.GetContract(contractId);
            switch (code)
            {
                case "Declarant":
                    contract.DeclarantId = declarant.Id;
                    contract.DeclarantDetailId = declarantDetail.Id;
                    break;
                case "Manufactur":
                    contract.ManufacturId = declarant.Id;
                    contract.ManufacturDetailId = declarantDetail.Id;
                    break;
                case "Payer":
                    contract.PayerId = declarant.Id;
                    contract.PayerDetailId = declarantDetail.Id;
                    break;
            }
            await _repo.UpdateContract(contract);
            return new { declarant.Id, DetailId = declarantDetail.Id };
        }

        /// <summary>
        /// Автосохранение договора
        /// </summary>
        /// <param name="contract">модель</param>
        /// <returns></returns>
        public async Task<object> ChangeModel(ContractUpdateModel contract)
        {         
            var data = Type.GetType(contract.ClassName + ", Teme.Shared.Data");
            List<Task> tasks = new List<Task>();
            Dictionary<string, string> objectList = JsonConvert.DeserializeObject<Dictionary<string, string>>(contract.Fields.ToString());
            foreach (var o in objectList)
            {
                tasks.Add(_repo.UpdateContractSql(data, o.Key, o.Value, contract.Id));
            }
            Task.WaitAll(tasks.ToArray());
            return new { contractId = contract.Id };
        }

        /// <summary>
        /// Сохранение данных калькулятора
        /// </summary>
        /// <returns></returns>
        public async Task<object> SaveCostWork(CostWorkModel[] costWorkModel)
        {
            List<Task> tasks = new List<Task>();
            foreach(var cw in costWorkModel)
            {
                var costwork = new Shared.Data.Context.CostWork()
                {
                    PriceListId = cw.PriceListId,
                    ContractId = cw.ContractId,
                    Count = cw.Count,
                    IsImport = cw.IsImport,
                    PriceWithValueAddedTax = cw.PriceWithValueAddedTax,
                    TotalPriceWithValueAddedTax = cw.TotalPriceWithValueAddedTax
                };
                tasks.Add(_repo.SaveCostWork(costwork));
            }
            Task.WaitAll(tasks.ToArray());
            return new { contractId = costWorkModel.First().ContractId };
        }

        /// <summary>
        /// Удаление сохраненных данных калькулятора
        /// </summary>
        /// <param name="contractId"></param>
        /// <returns></returns>
        public async Task DeleteCostWork(int contractId)
        {
            await _repo.DeleteCostWork(contractId);
        }

        //public async Task GetListContracts() //int userId
        //{
        //    await _repo.GetListContracts();
        //}
    }
}