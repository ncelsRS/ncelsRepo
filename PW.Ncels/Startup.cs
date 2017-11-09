using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(PW.Ncels.Startup))]
namespace PW.Ncels
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
