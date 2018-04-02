using Autofac;
using System.Linq;
using System.Reflection;

namespace RSC.IdentityServer4.Startups
{
    public class AutofacModule: Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            var assembly = Assembly.Load(new AssemblyName("Teme.Shared.Data"));
            builder.RegisterAssemblyTypes(assembly)
                   .Where(t => t.Name.EndsWith("Repo"))
                   .AsImplementedInterfaces();
            assembly = Assembly.Load(new AssemblyName("Teme.Identity.Data"));
            builder.RegisterAssemblyTypes(assembly)
                   .Where(t => t.Name.EndsWith("Repo"))
                   .AsImplementedInterfaces();

            assembly = Assembly.Load(new AssemblyName("Teme.Identity.Logic"));
            builder.RegisterAssemblyTypes(assembly)
                   .Where(t => t.Name.EndsWith("Logic"))
                   .AsImplementedInterfaces();
        }
    }
}
