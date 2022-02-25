using KhodiAsp.Security;
using Microsoft.Owin;
using Microsoft.Owin.Security.OAuth;
using Owin;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Web.Http;

[assembly: OwinStartup(typeof(KhodiAsp.Startup))]

namespace KhodiAsp
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            Debug.WriteLine("Startup Class Starting");
            // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=316888

            var authProvider = new TokenAuthenticatorProvider();
            OAuthAuthorizationServerOptions options = new OAuthAuthorizationServerOptions
            {
                AllowInsecureHttp = false,
                TokenEndpointPath = new PathString("/getAuthToken"),
                AccessTokenExpireTimeSpan = TimeSpan.FromMinutes(30),
                Provider = authProvider
            };
            app.UseOAuthAuthorizationServer(options);
            app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions());


            HttpConfiguration config = new HttpConfiguration();
            WebApiConfig.Register(config);
        }
    }
}
