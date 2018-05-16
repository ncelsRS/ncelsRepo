using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Teme.Payment.Api.Startups
{
    public class AutofacModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            const string assemblyBranch = "Payment";

            var assembly = Assembly.Load(new AssemblyName("Teme.Shared.Data"));
            builder.RegisterAssemblyTypes(assembly)
                .Where(t => t.Name.EndsWith("Repo"))
                .AsImplementedInterfaces();
            assembly = Assembly.Load(new AssemblyName($"Teme.{assemblyBranch}.Data"));
            builder.RegisterAssemblyTypes(assembly)
                .Where(t => t.Name.EndsWith("Repo"))
                .AsImplementedInterfaces();

            //assembly = Assembly.Load(new AssemblyName($"Teme.{assemblyBranch}.Infrastructure"));
            //builder.RegisterAssemblyTypes(assembly)
            //    .Where(t => t.Name.EndsWith("Logic"))
            //    .AsImplementedInterfaces();

            assembly = Assembly.Load(new AssemblyName($"Teme.{assemblyBranch}.Logic"));
            builder.RegisterAssemblyTypes(assembly)
                .Where(t => t.Name.EndsWith("Logic"))
                .AsImplementedInterfaces();
        }
    }
}
