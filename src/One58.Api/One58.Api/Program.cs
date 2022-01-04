using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.AzureAppConfiguration;
using Microsoft.Extensions.Hosting;

namespace One58.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.ConfigureAppConfiguration(config =>
                    {
                        var settings = config.Build();
                        
                        var appConfigurationConnectionString = settings["ConnectionStrings:AzureAppConfiguration"];
                        var filterKey = settings["FeatureFlagOptions:FilterKey"];
                        var filterLabel = settings["FeatureFlagOptions:FilterLabel"];

                        config.AddAzureAppConfiguration(options =>
                            options
                            .Connect(appConfigurationConnectionString)
                            .UseFeatureFlags(featureFlagOptions => {
                                featureFlagOptions.Select(filterKey, LabelFilter.Null);
                                featureFlagOptions.Select(KeyFilter.Any, filterLabel);
                            })
                            );
                    })
                    .UseStartup<Startup>();
                });
    }
}
