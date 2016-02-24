using Microsoft.Owin.Security.OAuth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;

namespace Wingman.Infrastructure
{
    public class WingmanAuthorizationServerProvider : OAuthAuthorizationServerProvider
    {
        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            context.Validated();
        }

        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            //Cors
            context.OwinContext.Response.Headers.Add("Access-Control-Allow-Origin", new[] { "*" });

            //Validate the User
            using (var authRepository = new AuthorizationRepository())
            {
                var user = await authRepository.FindUser(context.UserName, context.Password);

                if (user == null)
                {
                    context.SetError("invalid_grant", "The username or password is incorrect");

                    return;
                }
                else
                {
                    var token = new ClaimsIdentity(context.Options.AuthenticationType);

                    token.AddClaim(new Claim("sub", context.UserName));
                    token.AddClaim(new Claim("role", "user"));

                    context.Validated(token);
                }
            }
        }
    }
}