using Autofac;
using RSC.Core.EmailServer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

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

            builder.RegisterType<EmailSender>().AsSelf().InstancePerDependency();
        }
    }
}
