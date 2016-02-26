using Microsoft.Owin.Security.OAuth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using Wingman.Core.Infrastructure;

namespace Wingman.Infrastructure
{
    public class WingmanAuthorizationServerProvider : OAuthAuthorizationServerProvider
    {
        private readonly Func<IAuthorizationRepository> _authRepositoryFactory;

        private IAuthorizationRepository _authRepository
        {
            get
            {
                return _authRepositoryFactory.Invoke();
            }
        }

        public WingmanAuthorizationServerProvider(Func<IAuthorizationRepository> authRepositoryFactory)
        {
            _authRepositoryFactory = authRepositoryFactory;
        }

        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            context.Validated();
        }

        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            //Cors
            context.OwinContext.Response.Headers.Add("Access-Control-Allow-Origin", new[] { "*" });

            //Validate the User
            var user = await _authRepository.FindUser(context.UserName, context.Password);

            if (user == null)
            {
                context.SetError("invalid_grant", "The username or password is incorrect");

                return;
            }
            else
            {
                var token = new ClaimsIdentity(context.Options.AuthenticationType);

<<<<<<< HEAD
                token.AddClaim(new Claim(ClaimTypes.Name, context.UserName));
                token.AddClaim(new Claim("role", "user"));
=======
                    token.AddClaim(new Claim(ClaimTypes.Name, context.UserName));
                    token.AddClaim(new Claim("role", "user"));
>>>>>>> 1074105449302d0be0044c590d439d4b77bb438c

                context.Validated(token);
            }
        }
    }
}