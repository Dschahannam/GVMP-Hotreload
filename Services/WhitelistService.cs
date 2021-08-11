using GVMP_HotReload.Model;
using GVMP_HotReload.Utilities;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GVMP_HotReload.Services
{
    public sealed class WhitelistService : IHostedService
    {
        private readonly Logger _logger;
        private readonly IConfiguration _config;

        private readonly string AUTH_TOKEN;
        private readonly string NAME;
        private readonly string PROXY;
        private readonly uint FORUM_ID;

        public WhitelistService(IServiceProvider services, Logger logger)
        {
            _logger = logger;
            _config = services.GetRequiredService<IConfiguration>();

            AUTH_TOKEN = _config["auth_token"];
            NAME = _config["name"];
            PROXY = _config["server"];
            FORUM_ID = Convert.ToUInt32(_config["forum_id"]);
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.Console("Whitelist", $"{NAME} - Run whitelist service...");

            WhitelistDataModel value = new WhitelistDataModel(AUTH_TOKEN, NAME, PROXY, FORUM_ID);

            using WebClient webClient = new WebClient();
            webClient.Encoding = Encoding.UTF8;
            webClient.Headers["Content-Type"] = "application/json";

            string webResponse = await webClient.UploadStringTaskAsync("https://launcher.gvmp.de:5002/player/whitelist", JsonConvert.SerializeObject(value));
            if (webResponse.Contains("erfolgreich"))
                _logger.Console("Whitelist", $"You can relog now. ({PROXY})");
            else
                _logger.Console("Whitelist", $"Error: {JsonConvert.DeserializeObject<dynamic>(webResponse).reason}");
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.Console("Whitelist", "Stop whitelist service...");

            return Task.CompletedTask;
        }
    }
}
