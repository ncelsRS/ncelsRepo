using System.Threading.Tasks;
using IdentityServer4.Validation;

namespace RSC.IdentityServer4.IdentityConfig
{
    public class ExtensionGrantValidator : IExtensionGrantValidator
    {
        public string GrantType => "rsc";

        public async Task ValidateAsync(ExtensionGrantValidationContext context)
        {
            context.Result = new GrantValidationResult();
        }
    }
}