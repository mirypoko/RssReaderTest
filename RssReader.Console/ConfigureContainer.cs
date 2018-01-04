using Autofac;
using Domain.Services.EntityFramework;
using Domain.Services.Interfaces;
using Services;
using Services.Interfaces;

namespace RssReader.Console
{
    public class AutofacConfig
    {
        public static IContainer Container { get; set; }

        public static void ConfigureContainer()
        {
            var builder = new ContainerBuilder();

            builder.RegisterType(typeof(GenericUnitOfWork)).As(typeof(IGenericUnitOfWork));
            builder.RegisterType<RssService>().As<IRssService>();

            Container = builder.Build();
        }
    }
}
