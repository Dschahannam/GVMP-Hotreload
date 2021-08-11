using GVMP_HotReload.Services;
using GVMP_HotReload.Utilities;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;

namespace GVMP_HotReload
{
    public sealed class Startup
    {
        private readonly IConfiguration _config;
        public Startup()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location))
                .AddJsonFile("config.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables();

            _config = builder.Build();
        }

        public void InitializeServices(IServiceCollection services)
        {
            services.AddSingleton<Logger>();
            services.AddSingleton(_config);

            services.AddHostedService<WhitelistService>();
        }
    }
}
