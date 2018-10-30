using System;
using System.Net.Http;
using DotNetCore2018.SwaggerClient;

namespace DotNetCore2018.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:5000/api/v1/");

                var response = client.GetAsync("categories").Result;
                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine("Categories: ");
                    Console.WriteLine(response.Content.ReadAsStringAsync().Result);
                }
                response = client.GetAsync("products").Result;
                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine("Products: ");
                    Console.WriteLine(response.Content.ReadAsStringAsync().Result);
                }
            }

            var categoryClient = new DotNetCore2018CategoryApiClient();
            var categories = categoryClient.GetAllAsync().Result;
            foreach (var category in categories)
            {
                Console.WriteLine($"Category {{ Id: {category.Id}, Name: {category.Name} }}");
            }
        }
    }
}
