using IdentityServer3.Core.Configuration;
using IdentityServer3.Core.Services;
using Owin;
using System;
using System.Linq;
using System.Security.Cryptography.X509Certificates;

namespace RSC.IdentityServer3
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var factory = new IdentityServerServiceFactory()
                .UseInMemoryClients(Clients.Get())
                .UseInMemoryScopes(Scopes.Get());

            factory.UserService = new Registration<IUserService, LocalAuthenticationService>();

            //app.UseIdentityServer(new IdentityServerOptions
            //{
            //    SiteName = "Identity server",
            //    RequireSsl = false,
            //    SigningCertificate = LoadCertificate(),

            //    Factory = factory
            //});
            var repo = new RSC.DataModel.NcelsEntities();
            var employee = repo.OBK_ExpertCouncil.FirstOrDefault();
            Console.WriteLine(employee.Name);
        }

        private static X509Certificate2 LoadCertificate()
        {
            return new X509Certificate2();
            //Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"idsrv3test.pfx"), "idsrv3test");
        }
    }
}