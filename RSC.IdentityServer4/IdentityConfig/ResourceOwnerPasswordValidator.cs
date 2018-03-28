using System.Threading.Tasks;
using IdentityServer4.Validation;

namespace RSC.IdentityServer4.IdentityConfig
{
    public class ResourceOwnerPasswordValidator //: IResourceOwnerPasswordValidator
    {
        //public Task ValidateAsync(ResourceOwnerPasswordValidationContext context)
        //{
        //    try
        //    {
        //        //get your user model from db (by username - in my case its email)
        //        var user = await _userRepository.FindAsync(context.UserName);
        //        if (user != null)
        //        {
        //            //check if password match - remember to hash password if stored as hash in db
        //            if (user.Password == context.Password)
        //            {
        //                //set the result
        //                context.Result = new GrantValidationResult(
        //                    subject: user.UserId.ToString(),
        //                    authenticationMethod: "custom",
        //                    claims: GetUserClaims(user));

        //                return;
        //            }

        //            context.Result = new GrantValidationResult(TokenRequestErrors.InvalidGrant, "Incorrect password");
        //            return;
        //        }
        //        context.Result = new GrantValidationResult(TokenRequestErrors.InvalidGrant, "User does not exist.");
        //        return;
        //    }
        //    catch (Exception ex)
        //    {
        //        context.Result = new GrantValidationResult(TokenRequestErrors.InvalidGrant, "Invalid username or password");
        //    }
        //}
    }
}
