using System;
using System.Collections.Generic;
using System.Text;
using Autofac;

namespace RSC.FileStorageLogic
{
    public class AutofacModule:Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            builder.RegisterAssemblyTypes(ThisAssembly)
                .Where(x => x.Name.EndsWith("Logic"))
                .AsImplementedInterfaces();
        }
    }
}
