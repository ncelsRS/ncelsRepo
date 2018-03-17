using Microsoft.Owin;
using Microsoft.Owin.Security.Jwt;
using Owin;
using System;
using System.IO;
using System.Security.Cryptography.X509Certificates;

[assembly: OwinStartup(typeof(PW.Ncels.Startup))]
namespace PW.Ncels
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            //ConfigureAuth(app);
            var filename = "./identity.crt";
            var path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, filename);
            var cert = new X509Certificate2(path);

            app.UseJwtBearerAuthentication(new JwtBearerAuthenticationOptions
            {
                AllowedAudiences = new[] { "obk" },
                IssuerSecurityTokenProviders = new IIssuerSecurityTokenProvider[]
                {
                    new X509CertificateSecurityTokenProvider("http://localhost:9433", cert)
                }
            });

            //app.Use(new Func<Func<IDictionary<string, object>, Task>, Func<IDictionary<string, object>, Task>>(next => (async env =>
            //{
            //    Console.WriteLine("Begin Request");
            //    await next.Invoke(env);
            //    Console.WriteLine("End Request");
            //})));

            //app.UseCookieAuthentication(new CookieAuthenticationOptions
            //{
            //    AuthenticationType = "Cookies"
            //});
            //app.UseOpenIdConnectAuthentication(new OpenIdConnectAuthenticationOptions()
            //{
            //    Authority = "http://localhost:9433/",
            //    ClientId = "obk",
            //    RedirectUri = "http://localhost:9002/",
            //    ResponseType = "id_token",

            //    SignInAsAuthenticationType = "Cookies"
            //});
        }
    }
}
