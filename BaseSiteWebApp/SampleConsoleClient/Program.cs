using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace SampleConsoleClient
{
    class Program
    {        
        static void ShowProduct(Products product)
        {
            Console.WriteLine($"ProductId: {product.ProductId}\tName: {product.ProductName}\tPrice: {product.UnitPrice}\tCategory: {product.Category.CategoryName}");
        }
        static void ShowCategory(Categories category)
        {
            Console.WriteLine($"CategoryId: {category.CategoryId}\tName: {category.CategoryName}\tDescription: {category.Description}");
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Hello from console sample client! Pleaase wait for the response.");
            RunAsync().GetAwaiter().GetResult();
        }

        static async Task RunAsync()
        {            
            HttpClient client = new HttpClient
            {
                BaseAddress = new Uri("https://localhost:44354/")
            };
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
            try
            {
                //Get products
                var products = await Utility<IEnumerable<Products>>.GetItemsAsync(client, nameof(Products));
                Console.WriteLine("Products:");
                foreach (var product in products)
                    ShowProduct(product);
                Console.WriteLine();
                //Get categories
                var categoriesIndexViewModel = await Utility<CategoriesIndexViewModel>.GetItemsAsync(client, nameof(Categories));
                Console.WriteLine("Categories:");
                foreach (var category in categoriesIndexViewModel.Categories)
                    ShowCategory(category);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            Console.ReadLine();
        }
    }

    public static class Utility<T>
    {
        public static async Task<T> GetItemsAsync(HttpClient client, string controllerName)
        {
            T items = default(T);
            HttpResponseMessage response = await client.GetAsync($"api/{controllerName}");
            if (response.IsSuccessStatusCode)
                items = JsonConvert.DeserializeObject<T>(await response.Content.ReadAsStringAsync());
            return items;
        }
    }
}
