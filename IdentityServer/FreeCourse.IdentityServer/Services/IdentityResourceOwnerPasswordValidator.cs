using FreeCourse.IdentityServer.Models;
using IdentityModel;
using IdentityServer4.Validation;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FreeCourse.IdentityServer.Services
{
    public class IdentityResourceOwnerPasswordValidator : IResourceOwnerPasswordValidator
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public IdentityResourceOwnerPasswordValidator(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task ValidateAsync(ResourceOwnerPasswordValidationContext context)
        {
            var exitsUser = await _userManager.FindByNameAsync(context.UserName);
            if (exitsUser == null)
            {
                var errors = new Dictionary<string, object>();
                errors.Add("errors", new List<string> { "email veya şifreniz yanlış!" });
                context.Result.CustomResponse = errors;
                return;
            }

            var passwordCheck = await _userManager.CheckPasswordAsync(exitsUser, context.Password);
            if (passwordCheck == false)
            {
                var errors = new Dictionary<string, object>();
                errors.Add("errors", new List<string> { "Şifreniz yanlış!" });
                context.Result.CustomResponse = errors;
                return;
            }

            context.Result = new GrantValidationResult(exitsUser.Id.ToString(), OidcConstants.AuthenticationMethods.Password);
        }
    }
}
