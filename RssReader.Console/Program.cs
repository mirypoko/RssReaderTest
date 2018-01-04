using Autofac;
using Domain.Services.Interfaces;
using Services.Interfaces;
using System.Reflection;

namespace RssReader.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            AutofacConfig.ConfigureContainer();

            using (var scope = AutofacConfig.Container.BeginLifetimeScope())
            {
                var rssService = scope.Resolve<IRssService>();
                var consoleInterface = new ConsoleInterface(rssService);
                consoleInterface.Show();
            }
        }
    }
}
