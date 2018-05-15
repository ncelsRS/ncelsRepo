using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Teme.Contract.Api.Controllers;

namespace Teme.Contract.Api.Startups
{
    public class AutofacModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            const string assemblyBranch = "Contract";

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
