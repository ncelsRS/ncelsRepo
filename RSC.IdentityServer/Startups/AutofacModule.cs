using System.Reflection;
using Autofac;
using RSC.Core.EmailServer;
using Teme.Identity.Logic;

namespace RSC.IdentityServer.Startups
{
    public class AutofacModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            var assembly = Assembly.Load(new AssemblyName("Teme.Shared.Data"));
            builder.RegisterAssemblyTypes(assembly)
                .Where(t => t.Name.EndsWith("Repo"))
                .AsImplementedInterfaces();
            assembly = Assembly.Load(new AssemblyName("Teme.Shared.Logic"));
            builder.RegisterAssemblyTypes(assembly)
                .Where(t => t.Name.EndsWith("Logic"))
                .AsImplementedInterfaces();

            assembly = Assembly.Load(new AssemblyName("Teme.Identity.Data"));
            builder.RegisterAssemblyTypes(assembly)
                .Where(t => t.Name.EndsWith("Repo"))
                .AsImplementedInterfaces();
            assembly = Assembly.Load(new AssemblyName("Teme.Identity.Logic"));
            builder.RegisterAssemblyTypes(assembly)
                .Where(t => t.Name.EndsWith("Logic"))
                .AsImplementedInterfaces();

            builder.RegisterType<EmailSender>().AsSelf().InstancePerDependency();

        }
    }
}