using Enrichments.Contracts;
using Microsoft.Extensions.Logging;
using Orleans;
using Orleans.Configuration;
using System;
using System.Threading.Tasks;

namespace Enrichments.TestClient
{
    class Program
    {
        static async Task Main(string[] args)
        {            
            var client = CreateOrleansClient();
            var g = client.GetGrain<IVisitor>("test");
            await g.Echo();
        }


        private static IClusterClient CreateOrleansClient()
        {
            var clientBuilder = new ClientBuilder()
                .UseLocalhostClustering()
                .Configure<ClusterOptions>(options =>
                {
                    options.ClusterId = "dev";
                    options.ServiceId = "Orleans2DependencyInjection";
                })
                .ConfigureLogging(logging => logging.AddConsole());

            var client = clientBuilder.Build();

            client.Connect(async ex =>
            {
                Console.WriteLine("Retrying...");
                await Task.Delay(3000);
                return true;
            }).Wait();

            return client;
        }
    }
}
