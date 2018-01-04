using Autofac;
using Autofac.Integration.Mvc;
using Domain.Services.EntityFramework;
using Domain.Services.Interfaces;
using Services;
using Services.Interfaces;
using System.Web.Mvc;

namespace RssReader.Mvc
{
    public class AutofacConfig
    {
        public static IContainer Container { get; set; }

        public static void ConfigureContainer()
        {
            var builder = new ContainerBuilder();

            builder.RegisterControllers(typeof(MvcApplication).Assembly);

            builder.RegisterType(typeof(GenericUnitOfWork)).As(typeof(IGenericUnitOfWork));
            builder.RegisterType<RssService>().As<IRssService>();

            Container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(Container));
        }
    }
}
