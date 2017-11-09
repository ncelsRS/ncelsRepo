using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PW.Ncels.Database.DataModel;

namespace PW.Ncels.Database.Repository.Expertise
{
    public class ReestrDirectionToPayRepository : ARepositoryGeneric<ReestrDirectionToPayView>
    {
        public ReestrDirectionToPayRepository(bool isProxy = true):base(isProxy) 
        {
            
        }
    }
}
