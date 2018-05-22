using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Teme.Infrastructure.Api.Startups
{
    public class AutofacModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            var assembly = Assembly.Load(new AssemblyName("Teme.Shared.Data"));
            builder.RegisterAssemblyTypes(assembly)
                .Where(t => t.Name.EndsWith("Repo"))
                .AsImplementedInterfaces();

            assembly = Assembly.Load(new AssemblyName("Teme.Contract.Data"));
            builder.RegisterAssemblyTypes(assembly)
                .Where(t => t.Name.EndsWith("Repo"))
                .AsImplementedInterfaces();

            assembly = Assembly.Load(new AssemblyName("Teme.Contract.Infrastructure"));
            builder.RegisterAssemblyTypes(assembly)
                .Where(t => t.Name.EndsWith("Logic"))
                .AsImplementedInterfaces();

            assembly = Assembly.Load(new AssemblyName("Teme.Declaration.Infrastructure"));
            builder.RegisterAssemblyTypes(assembly)
                .Where(t => t.Name.EndsWith("Logic"))
                .AsImplementedInterfaces();

            assembly = Assembly.Load(new AssemblyName("Teme.Infrastructure.Logic"));
            builder.RegisterAssemblyTypes(assembly)
                .Where(t => t.Name.EndsWith("Logic"))
                .AsImplementedInterfaces();
        }
    }
}
