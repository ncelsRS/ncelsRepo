using System;
using System.Linq;
using PW.Ncels.Database.DataModel;

namespace PW.Ncels.Database.Repository.Security
{
    /// <summary>
    /// Работа с данными, Пользователи
    /// </summary>
    public class EmployeesRepository : ARepository
    {
        public Employee GetById(Guid? id)
        {
            return AppContext.Employees.FirstOrDefault(e => e.Id == id);
        }
    }
}
