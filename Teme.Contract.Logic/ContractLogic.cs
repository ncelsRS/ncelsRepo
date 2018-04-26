using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Teme.Contract.Data;
using Teme.Contract.Data.Model;
using Teme.Contract.Infrastructure;
using Teme.Contract.Infrastructure.Primitives;
using Teme.Contract.Infrastructure.Workflow;
using Teme.Shared.Data.Primitives.Contract;
using Teme.Shared.Data.Repos.ContractRepo;
using Teme.Shared.Logic.ContractLogic;

namespace Teme.Contract.Logic
{
    public class ContractLogic : BaseContractLogic<IContractRepo>, IContractLogic
    {
        private readonly IContractWorkflowLogic _wflogic;
        private readonly IContractRepo _repo;

        public ContractLogic(IContractRepo repo, IContractWorkflowLogic wflogic) : base(repo)
        {
            _wflogic = wflogic;
            _repo = repo;
        }

        public async Task<object> Create()
        {
            var workflowId = await _wflogic.Create();
            var qwe = workflowId.GetType().GetProperty("workflowId").GetValue(workflowId).ToString();
            Shared.Data.Context.Contract contract = new Shared.Data.Context.Contract()
            {
                WorkflowId = qwe
            };
            await _repo.CreateContract(contract);            
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

        public async Task<object> SearchDeclarantResident(string iin)
        {
            var declarant = await _repo.SearchDeclarantResident(iin);
            if(declarant != null)
            {
                var detail = declarant.DeclarantDetails.OrderByDescending(e => e.DateCreate).FirstOrDefault();
                return new DeclarantModel {
                    Id = declarant.Id,
                    IdNumber = declarant.IdNumber,
                    NameRu = declarant.NameRu,
                    NameKz = declarant.NameKz,
                    NameEn = declarant.NameEn,
                    CountryId = declarant.CountryId,
                    OrganizationFormId = declarant.OrganizationFormId,
                    IsResident = declarant.IsResident,
                    DeclarantDetailModel = new DeclarantDetailModel
                    {
                        Id = detail.Id,
                        DeclarantId = detail.DeclarantId,
                        LegalAddress = detail.LegalAddress,
                        FactAddress = detail.FactAddress,
                        BossLastName = detail.BossLastName,
                        BossFirstName = detail.BossFirstName,
                        BossMiddleName = detail.BossMiddleName,
                        BossPositionRu = detail.BossPositionRu,
                        BossPositionKz = detail.BossPositionKz,
                        BankName = detail.BankName,
                        BankIik = detail.BankIik,
                        BankSwift = detail.BankSwift,
                        BankBin =detail.BankBin,
                        CurrencyId = detail.CurrencyId,
                        Phone = detail.Phone,
                        Phone2 = detail.Phone2,
                        Email = detail.Email,
                        DeclarantDocType = detail.DeclarantDocType,
                        DeclarantDocWithoutNumber =detail.DeclarantDocWithoutNumber,
                        DeclarantDocNumber = detail.DeclarantDocNumber,
                        DeclarantDocStartDate = detail.DeclarantDocStartDate,
                        DeclarantDocEndDate = detail.DeclarantDocEndDate,
                        DeclarantPerpetualDoc = detail.DeclarantPerpetualDoc
                    }
                };
            }
            return null;
        }

        public async Task<object> SearchDeclarantNonResident(int countryId)
        {
            var declarant = await _repo.SearchDeclarantNonResident(countryId);
            if (declarant.Any())
                return declarant.Select(e=> new { e.Id, e.NameRu, e.NameKz, e.NameEn });
            return null;
        }

        public async Task<object> GetDeclarantById(int id)
        {
            var declarant = await _repo.GetDeclarant(id);
            if (declarant != null)
            {
                var detail = declarant.DeclarantDetails.OrderByDescending(e => e.DateCreate).FirstOrDefault();
                return new DeclarantModel
                {
                    Id = declarant.Id,
                    IdNumber = declarant.IdNumber,
                    NameRu = declarant.NameRu,
                    NameKz = declarant.NameKz,
                    NameEn = declarant.NameEn,
                    CountryId = declarant.CountryId,
                    OrganizationFormId = declarant.OrganizationFormId,
                    IsResident = declarant.IsResident,
                    DeclarantDetailModel = new DeclarantDetailModel
                    {
                        Id = detail.Id,
                        DeclarantId = detail.DeclarantId,
                        LegalAddress = detail.LegalAddress,
                        FactAddress = detail.FactAddress,
                        BossLastName = detail.BossLastName,
                        BossFirstName = detail.BossFirstName,
                        BossMiddleName = detail.BossMiddleName,
                        BossPositionRu = detail.BossPositionRu,
                        BossPositionKz = detail.BossPositionKz,
                        BankName = detail.BankName,
                        BankIik = detail.BankIik,
                        BankSwift = detail.BankSwift,
                        BankBin = detail.BankBin,
                        CurrencyId = detail.CurrencyId,
                        Phone = detail.Phone,
                        Phone2 = detail.Phone2,
                        Email = detail.Email,
                        DeclarantDocType = detail.DeclarantDocType,
                        DeclarantDocWithoutNumber = detail.DeclarantDocWithoutNumber,
                        DeclarantDocNumber = detail.DeclarantDocNumber,
                        DeclarantDocStartDate = detail.DeclarantDocStartDate,
                        DeclarantDocEndDate = detail.DeclarantDocEndDate,
                        DeclarantPerpetualDoc = detail.DeclarantPerpetualDoc
                    }
                };
            }
            return null;
        }

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
    }
}