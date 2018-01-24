using PW.Ncels.Database.DataModel;
using PW.Ncels.Database.Helpers;
using PW.Ncels.Database.Models.OBK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic;

namespace PW.Ncels.Database.Repository.OBK
{

    public class BlankAccountingRepository : ARepository
    {
        public IQueryable<OBK_BlankAccountingView> List()
        {
            return AppContext.OBK_BlankAccountingView;
        }
       
        public void Decommission(List<Guid> list)
        {
            var blanks = AppContext.OBK_BlankNumber.Where(o => list.Contains(o.Id));
            foreach (var temp in blanks)
            {
                temp.Decommissioned = true;
            }
            AppContext.SaveChanges();
        }

        public IQueryable<OBK_CorruptedBlankNumberView> CorruptedUserBlanks()
        {
            var userId = UserHelper.GetCurrentEmployee().Id;
            return AppContext.OBK_CorruptedBlankNumberView.Where(o => o.CorruptEmployee == userId);
        }

        public OBK_BlankResponse SaveCorrupted(int number, DateTime date)
        {
            var blankNumber = AppContext.OBK_BlankNumber.FirstOrDefault(o => o.Number == number);
            if (blankNumber == null)
            {
                return new OBK_BlankResponse()
                {
                    response = false,
                    message = "Бланк с таким номером не существует!"
                };
            }

            blankNumber.Corrupted = true;
            blankNumber.CorruptEmployee = UserHelper.GetCurrentEmployee().Id;
            blankNumber.CorruptDate = DateTime.Now;

            AppContext.SaveChanges();

            return new OBK_BlankResponse()
            {
                response = true,
                message = "Успешно добавлен!"
            };
        }
    }
}
