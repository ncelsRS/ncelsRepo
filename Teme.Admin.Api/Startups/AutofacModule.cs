using Autofac;
using System.Linq;
using System.Reflection;

namespace Teme.Admin.Api.Startups
{
    public class AutofacModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            var assemblyBranch = "Admin";

            var assembly = Assembly.Load(new AssemblyName("Teme.Shared.Data"));
            builder.RegisterAssemblyTypes(assembly)
                       .Where(t => t.Name.EndsWith("Repo"))
                       .AsImplementedInterfaces();
            assembly = Assembly.Load(new AssemblyName($"Teme.{assemblyBranch}.Data"));
            builder.RegisterAssemblyTypes(assembly)
                   .Where(t => t.Name.EndsWith("Repo"))
                   .AsImplementedInterfaces();

            assembly = Assembly.Load(new AssemblyName($"Teme.{assemblyBranch}.Logic"));
            builder.RegisterAssemblyTypes(assembly)
                   .Where(t => t.Name.EndsWith("Logic"))
                   .AsSelf();
        }
    }
}
