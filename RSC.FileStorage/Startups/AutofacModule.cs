using Autofac;
using RSC.Core.EmailServer;
using System.Reflection;

namespace RSC.FileStorage.Startups
{
    public class AutofacModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            builder.RegisterModule<FileStorageData.AutofacModule>();
            builder.RegisterModule<FileStorageLogic.AutofacModule>();
        }
    }
}
