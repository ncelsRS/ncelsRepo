using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ncels.Helpers.Config
{
    public static class GlobalConfigs
    {
        public static bool ExpertiseEnabled
        {
            get
            {
                var value = ConfigurationManager.AppSettings.Get("ExpertiseEnabled");
                if (value == null)
                    return false;
                bool result;
                bool.TryParse(value, out result);
                return result;
            }
        }
    }
}
