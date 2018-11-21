using Enrichments.Contracts;
using Enrichments.Domains;
using Orleans.Configuration;
using Orleans.Hosting;
using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Orleans.Runtime.Configuration;
using Orleans;
using Microsoft.Extensions.Logging;
using Enrichments.Grains;

namespace Enrichments.Host
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var host = buildSilo();
            await host.StartAsync();

            Console.ReadKey();
            await host.StopAsync();
        }

        private static ISiloHost buildSilo()
        {
            var siloBuilder = new SiloHostBuilder()
                .UseLocalhostClustering()
                .UseDashboard(options => { })
                .Configure<ClusterOptions>(options =>
                {
                    options.ClusterId = "dev";
                    options.ServiceId = "Orleans2DependencyInjection";
                })
                .Configure<EndpointOptions>(options =>
                    options.AdvertisedIPAddress = IPAddress.Loopback)
                .ConfigureApplicationParts(parts => parts.AddApplicationPart(typeof(VisitorGrain).Assembly).WithReferences())
                .ConfigureServices(services =>
                {
                    services.AddSingleton<IStreamService, KinesisStreamService>();
                })
                .ConfigureLogging(logging => logging.AddConsole());

            return siloBuilder.Build();
        }
    }
}
