using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;
using King_project.Entites;
using Microsoft.Net.Http.Headers;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;

namespace King_project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        static readonly HttpClient client = new();
        readonly JsonSerializerOptions options = new() { PropertyNameCaseInsensitive = true };

        [HttpGet,Authorize]
        public async Task<IActionResult> GetAllProducts()
        {
            Uri uri = new("https://dummyjson.com/products?select=id,title,price,description,images,category");
            HttpResponseMessage response = await client.GetAsync(uri).ConfigureAwait(false);

            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();



                var products = JsonSerializer.Deserialize<ProductsResponse>(json, options);
                return Ok(products);
            }
            Console.WriteLine("Bad request!");
            return BadRequest(response.ReasonPhrase);
        }

        [HttpGet("{id}"),Authorize]
        public async Task<IActionResult> GetProduct(int id)
        {
            Uri uri = new("https://dummyjson.com/products/" + id.ToString() + "?select=id,title,price,description,images,category");

            HttpResponseMessage response = await client.GetAsync(uri).ConfigureAwait(false);

            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();



                var product = JsonSerializer.Deserialize<Product>(json, options);
                return Ok(product);
            }
            Console.WriteLine("Bad request!");
            return BadRequest(response.ReasonPhrase);
        }

        [HttpGet("{category}/{priceLimit}"),Authorize]
        public async Task<IActionResult> GetProductsByCategoryAndPrice(string category, int priceLimit)
        {

            HttpResponseMessage categories = await client.GetAsync("https://dummyjson.com/products/category-list").ConfigureAwait(false);

            var categoriesJSON = await categories.Content.ReadAsStringAsync();
            
            if (categoriesJSON.Contains(category) && priceLimit > 0)
            {
                Uri uri = new("https://dummyjson.com/products/category/" + category + "?select=id,title,price,description,images,category");

                HttpResponseMessage response = await client.GetAsync(uri).ConfigureAwait(false);

                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();



                    var products = JsonSerializer.Deserialize<ProductsResponse>(json, options);

                    var filteredProducts = products.Products.Where(product => product.Price <= priceLimit);

                    return Ok(filteredProducts);
                }
                Console.WriteLine("Bad request!");
                return BadRequest(response.ReasonPhrase);
            }

            return BadRequest("Category doesn't exist or price limit is lesser than zero!");
        }

        [HttpGet("search/{search}"),Authorize]
        public async Task<IActionResult> GetProductsBySearch(string search)
        {
            Uri uri = new("https://dummyjson.com/products/search?q=" + search + "&select=id,title,price,description,images,category");

            HttpResponseMessage response = await client.GetAsync(uri).ConfigureAwait(false);

            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();



                var product = JsonSerializer.Deserialize<ProductsResponse>(json, options);
                return Ok(product);
            }
            Console.WriteLine("Bad request!");
            return BadRequest(response.ReasonPhrase);

        }
    }

    public class ProductsResponse
    {
        public required List<Product> Products { get; set; }
    }
}
