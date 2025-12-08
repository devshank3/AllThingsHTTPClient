using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using ConsoleHttpClient.Models;
using Newtonsoft.Json;

namespace ConsoleHttpClient.HttpClientTest
{
    /// <summary>
    /// Comprehensive HttpClient examples demonstrating:
    /// - All HTTP verbs (GET, POST, PUT, PATCH, DELETE, HEAD, OPTIONS)
    /// - Timeout configuration
    /// - Request/Response customization
    /// - Headers manipulation
    /// - Error handling
    /// </summary>
    public class HttpClientExamples
    {
        private const string BaseUrl = "https://localhost:7148";

        /// <summary>
        /// Demonstrates HttpClient configuration options
        /// DEFAULT TIMEOUT: 100 seconds
        /// </summary>
        public static void DemonstrateHttpClientConfiguration()
        {
            Console.WriteLine("\n=== HttpClient Configuration Options ===\n");

            using (var client = new HttpClient())
            {
                // 1. Base Address - prepended to all relative URIs
                client.BaseAddress = new Uri(BaseUrl);
                Console.WriteLine($"BaseAddress: {client.BaseAddress}");

                // 2. Timeout - Default is 100 seconds
                Console.WriteLine($"Default Timeout: {client.Timeout.TotalSeconds} seconds");
                client.Timeout = TimeSpan.FromSeconds(30);
                Console.WriteLine($"Custom Timeout: {client.Timeout.TotalSeconds} seconds");

                // 3. MaxResponseContentBufferSize - max bytes to buffer (default: ~2GB)
                Console.WriteLine($"MaxResponseContentBufferSize: {client.MaxResponseContentBufferSize} bytes");
                client.MaxResponseContentBufferSize = 1024 * 1024; // 1MB

                // 4. Default Request Headers - sent with EVERY request
                client.DefaultRequestHeaders.Add("User-Agent", "HttpClient-Demo/1.0");
                client.DefaultRequestHeaders.Add("X-Custom-Header", "CustomValue");
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                Console.WriteLine("\nDefault Request Headers:");
                foreach (var header in client.DefaultRequestHeaders)
                {
                    Console.WriteLine($"  {header.Key}: {string.Join(", ", header.Value)}");
                }
            }
        }

        /// <summary>
        /// GET request - Retrieve resources
        /// Demonstrates: query parameters, custom headers, response reading
        /// </summary>
        public static async Task DemonstrateGetRequest()
        {
            Console.WriteLine("\n=== GET Request Examples ===\n");

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(BaseUrl);
                client.Timeout = TimeSpan.FromSeconds(10);

                // Example 1: Simple GET
                Console.WriteLine("1. Simple GET all todos:");
                var response = await client.GetAsync("api/todo");
                Console.WriteLine($"   Status: {response.StatusCode}");

                var content = await response.Content.ReadAsStringAsync();
                var todos = JsonConvert.DeserializeObject<List<TodoItem>>(content);
                Console.WriteLine($"   Received {todos.Count} todos");

                // Example 2: GET with query parameters
                Console.WriteLine("\n2. GET with query parameter (delay):");
                var urlWithQuery = "api/todo?delay=2000";
                response = await client.GetAsync(urlWithQuery);
                Console.WriteLine($"   Status: {response.StatusCode}");

                // Example 3: GET specific resource
                Console.WriteLine("\n3. GET specific todo by ID:");
                response = await client.GetAsync("api/todo/1");
                Console.WriteLine($"   Status: {response.StatusCode}");

                content = await response.Content.ReadAsStringAsync();
                var todo = JsonConvert.DeserializeObject<TodoItem>(content);
                Console.WriteLine($"   Todo: {todo.Title}");

                // Example 4: Reading response headers
                Console.WriteLine("\n4. Response Headers:");
                foreach (var header in response.Headers)
                {
                    Console.WriteLine($"   {header.Key}: {string.Join(", ", header.Value)}");
                }
            }
        }
    }
}
