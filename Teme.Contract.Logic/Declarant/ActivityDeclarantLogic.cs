using System.Threading.Tasks;
using Teme.Contract.Infrastructure;

namespace Teme.Contract.Logic.Declarant
{
    public class ActivityDeclarantLogic : IActivityDeclarantLogic
    {
        private readonly string _repo;

        public ActivityDeclarantLogic(string repo)
        {
            _repo = repo;
        }
    }
}
