using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Teme.Contract.Data;
using Teme.Contract.Data.DTO;
using Teme.Contract.Infrastructure;
using Teme.Contract.Infrastructure.Primitives;
using Teme.Contract.Infrastructure.Primitives.Enums;
using Teme.ContractCoz.Data;
using Teme.Shared.Data.Context;
using Teme.Shared.Data.Repos;
using Teme.Shared.Logic;
using Teme.Shared.Logic.ContractLogic;

namespace Teme.ContractCoz.Logic
{

    public class ContractCozLogic : BaseContractLogic<IContractCozRepo>, IContractCozLogic
    {
        private readonly IContractRepo _contractRepo;
        private readonly IConvertDtoRepo _dto;
        public ContractCozLogic(IContractCozRepo repo, IContractRepo contractRepo, IConvertDtoRepo dto) : base(repo)
        {
            _contractRepo = contractRepo;
            _dto = dto;
        }

        /// <summary>
        /// Получение договора по Id
        /// </summary>
        /// <returns></returns>
        public async Task<object> GetContractById(int contractId)
        {
            var result = await _contractRepo.GetContract(contractId);
            if (result == null)
                return null;
            return _dto.ConvertEntityToContract(result);
        }

        /// <summary>
        /// Получение заявителя по Id
        /// </summary>
        /// <param name="id">Id заявителя</param>
        /// <returns></returns>
        public async Task<object> GetDeclarantById(int id)
        {
            var declarant = await _contractRepo.GetDeclarant(id);
            if (declarant != null)
                return _dto.ConvertEntityToDeclarant(declarant);
            return null;
        }

        /// <summary>
        /// Получение списка договоров
        /// </summary>
        /// <returns></returns>
        public async Task<object> GetListContract(string statusCode, int userId)
        {
            var user = GetUser(userId).Select(x => x.UserRoles.Select(e => e.Role.Permissions));
            return await Repo.GetListContract(statusCode, 4);
        }
    }
}
