using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PW.Ncels.Database.DataModel;

namespace PW.Ncels.Database.Repository
{
    public class SettingsRepository : ARepository
    {
        public Setting GetByCode(string code)
        {
            return AppContext.Settings.FirstOrDefault(e => e.UniqueName == code);

        }
    }
}
