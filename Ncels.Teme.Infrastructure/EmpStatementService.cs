using System.Collections.Generic;
using System.Linq;
using Ncels.Teme.Contracts;
using Ncels.Teme.Contracts.ViewModels;
using PW.Ncels.Database.DataModel;

namespace Ncels.Teme.Infrastructure
{
    public class EmpStatementService : IEmpStatementService
    {
        private IUnitOfWork _uow;

        public EmpStatementService()
        {
            _uow = new UnitOfWork();
        }

        public IEnumerable<EmpStatementViewModel> GetStatements()
        {
            return _uow.GetQueryable<EMP_Statement>()
                .Select(x => new EmpStatementViewModel
                {
                    Kind = x.RegistrationKindValue == "1"
                        ? "Регистрация"
                        : x.RegistrationKindValue == "2"
                            ? "Перерегистрация"
                            : "Внесение изменений",
                    Type = x.RegistrationTypeValue == "1" ? "Ускоренная" : string.Empty,
                    Name = x.MedicalDeviceNameRu,
                    Number = x.RegistrationCertificateNumber,
                    Date = x.RegistrationDate,
                }).ToList();
        }
    }
}
