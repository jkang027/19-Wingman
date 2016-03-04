using Microsoft.AspNet.Identity;
using Microsoft.Owin;
using Microsoft.Owin.Cors;
using Microsoft.Owin.Security.OAuth;
using Owin;
using SimpleInjector;
using SimpleInjector.Extensions.ExecutionContextScoping;
using SimpleInjector.Integration.WebApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using Wingman;
using Wingman.Core.Domain;
using Wingman.Core.Infrastructure;
using Wingman.Core.Repository;
using Wingman.Core.Services.Finance;
using Wingman.Data.Infrastructure;
using Wingman.Data.Repository;
using Wingman.Infrastructure;

[assembly: OwinStartup(typeof(PropertyManager.Api.Startup))]
namespace PropertyManager.Api
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var container = ConfigureSimpleInjector(app);
            ConfigureOAuth(app, container);

            HttpConfiguration config = new HttpConfiguration
            {
                DependencyResolver = new SimpleInjectorWebApiDependencyResolver(container)
            };

            WebApiConfig.Register(config);
            app.UseCors(CorsOptions.AllowAll);
            app.UseWebApi(config);
        }

        public void ConfigureOAuth(IAppBuilder app, Container container)
        {
            Func<IAuthorizationRepository> authRepositoryFactory = container.GetInstance<IAuthorizationRepository>;

            var authorizationOptions = new OAuthAuthorizationServerOptions
            {
                AllowInsecureHttp = true,
                TokenEndpointPath = new PathString("/api/token"),
                AccessTokenExpireTimeSpan = TimeSpan.FromDays(1),
                Provider = new WingmanAuthorizationServerProvider(authRepositoryFactory)
            };

            // Token Generation
            app.UseOAuthAuthorizationServer(authorizationOptions);
            app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions());
        }

        public Container ConfigureSimpleInjector(IAppBuilder app)
        {
            var container = new Container();

            container.Options.DefaultScopedLifestyle = new ExecutionContextScopeLifestyle();

            container.Register<IDatabaseFactory, DatabaseFactory>(Lifestyle.Scoped);
            container.Register<IUnitOfWork, UnitOfWork>();

            container.Register<IResponseRepository, ResponseRepository>();
            container.Register<IRoleRepository, RoleRepository>();
            container.Register<ISubmissionRepository, SubmissionRepository>();
            container.Register<ITopicRepository, TopicRepository>();
            container.Register<IUserRoleRepository, UserRoleRepository>();
            container.Register<IWingmanUserRepository, WingmanUserRepository>();
            container.Register<IUserStore<WingmanUser, string>, UserStore>(Lifestyle.Scoped);
            container.Register<IAuthorizationRepository, AuthorizationRepository>(Lifestyle.Scoped);
            container.Register<IKeyPaymentService, StripeKeyPaymentService>();

            // more code to facilitate a scoped lifestyle
            app.Use(async (context, next) => 
            {
                using (container.BeginExecutionContextScope())
                {
                    await next();
                }
            });

            container.Verify();

            return container;
        }
    }
}