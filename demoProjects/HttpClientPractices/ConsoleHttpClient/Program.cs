using ConsoleHttpClient.HttpClientTest;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace ConsoleHttpClient
{
    internal class Program
    {
        static void Main(string[] args)
        {
            MainAsync().GetAwaiter().GetResult();
        }

        //static async Task MainAsync()
        //{
        //    Console.WriteLine("Starting connections");
        //    for (int i = 0; i < 10; i++)
        //    {
        //        using (var client = new HttpClient())
        //        {
        //            // Add default headers - these will be sent with EVERY request
        //            client.DefaultRequestHeaders.Add("X-API-Key", "my-secret-api-key-123");
        //            client.DefaultRequestHeaders.Add("X-Client-ID", "ConsoleApp-v1.0");
        //            client.DefaultRequestHeaders.Add("User-Agent", "HttpClientDemo/1.0");

        //            var result = await client.GetAsync("https://localhost:7148/WeatherForecast");
        //            Console.WriteLine($"Request {i + 1}: {result.StatusCode}");
        //        }
        //    }
        //    Console.WriteLine("Connections done");
        //    Console.ReadLine();
        //}

        static async Task MainAsync()
        {
            Console.WriteLine("=================================================");
            Console.WriteLine("   HttpClient Comprehensive Examples");
            Console.WriteLine("   Make sure server is running on localhost:7148");
            Console.WriteLine("=================================================");

            try
            {
                // 1. Configuration
                HttpClientExamples.DemonstrateHttpClientConfiguration();
                await Task.Delay(1000);

                // 2. GET requests
                await HttpClientExamples.DemonstrateGetRequest();
                await Task.Delay(1000);

                

                Console.WriteLine("\n=================================================");
                Console.WriteLine("   All examples completed!");
                Console.WriteLine("=================================================");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\nError: {ex.Message}");
                Console.WriteLine($"Stack Trace: {ex.StackTrace}");
            }

            Console.WriteLine("\nPress any key to exit...");
            Console.ReadLine();
        }
    }
}