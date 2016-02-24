using Microsoft.Owin;
using Microsoft.Owin.Cors;
using Microsoft.Owin.Security.OAuth;
using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using Wingman;
using Wingman.Infrastructure;

[assembly: OwinStartup(typeof(PropertyManager.Api.Startup))]
namespace PropertyManager.Api
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureOAuth(app);

            HttpConfiguration config = new HttpConfiguration();
            WebApiConfig.Register(config);

            app.UseCors(CorsOptions.AllowAll);
            app.UseWebApi(config);
        }

        public void ConfigureOAuth(IAppBuilder app)
        {
            //Configure Authentication
            var authenticationOptions = new OAuthBearerAuthenticationOptions();
            app.UseOAuthBearerAuthentication(authenticationOptions);

            //Configure Authorization
            var authorizationOptions = new OAuthAuthorizationServerOptions
            {
                AllowInsecureHttp = true,
                TokenEndpointPath = new PathString("/api/token"),
                AccessTokenExpireTimeSpan = TimeSpan.FromDays(1),
                Provider = new WingmanAuthorizationServerProvider()
            };
            app.UseOAuthAuthorizationServer(authorizationOptions);
        }
    }
}