using ConsoleApp.Helpers;
using ConsoleApp.Models;
using ConsoleApp.Service;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace ConsoleApp
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var config = new ConfigurationBuilder()
              .AddJsonFile("appsettings.json", true, true)
              .Build();

            var serviceProvider = new ServiceCollection()
                        .AddSingleton<HttpClient>()
                        .BuildServiceProvider();

            var apiUrl = config["apiUrl"];

            var httpClient = serviceProvider.GetService<HttpClient>();
            var reader = new ApiReader(httpClient);
            // Read with specific shape
            //var metaWeather = await reader.ReadFromUrlAsync<MetaWeather>(apiUrl);
            //WeatherHelper.PrintInfo(metaWeather);

            // Read dynamically
            var jsonString = await reader.ReadAsString(apiUrl);
            DynamicJsonReaderHelper.ReadFromString(jsonString);

            Console.ReadKey();
        }
    }
}
