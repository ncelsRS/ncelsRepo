using Autofac;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace RSC.FileStorageData
{
    public class AutofacModule: Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);
            builder.RegisterAssemblyTypes(ThisAssembly)
                .Where(t => t.Name.EndsWith("Repo"))
                .AsImplementedInterfaces();
        }
    }
}
