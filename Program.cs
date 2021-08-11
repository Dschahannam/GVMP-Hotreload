#pragma warning disable IDE0060
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading;

namespace GVMP_HotReload
{
    public sealed class Program
    {
        private static IServiceProvider _serviceProvider;
        private static Startup _startup;

        static void Main(string[] args)
        {
            var serviceCollection = new ServiceCollection();
            _startup = new Startup();
            _startup.InitializeServices(serviceCollection);

            _serviceProvider = serviceCollection.BuildServiceProvider();

            foreach (var services in _serviceProvider.GetServices<IHostedService>())
            {
                services.StartAsync(CancellationToken.None);
            }

            AppDomain.CurrentDomain.ProcessExit += CurrentDomain_ProcessExit;

            // to avoid instant close yk
            Console.ReadLine();
        }

        private static void CurrentDomain_ProcessExit(object sender, EventArgs e)
        {
            foreach (var services in _serviceProvider.GetServices<IHostedService>())
            {
                services.StopAsync(CancellationToken.None);
            }
        }
    }
}
