using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;
using King_project.Entites;
using Microsoft.Net.Http.Headers;
using System.Net.Http.Headers;
using System.Text.Json;

namespace King_project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        static readonly HttpClient client = new();
        readonly JsonSerializerOptions options = new() { PropertyNameCaseInsensitive = true };

        [HttpGet]
        public async Task<IActionResult> GetAllProducts()
        {
            Uri uri = new("https://dummyjson.com/products?select=id,title,price,description,images");
            HttpResponseMessage response = await client.GetAsync(uri).ConfigureAwait(false);

            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();

                

                var products = JsonSerializer.Deserialize<ProductsResponse>(json, options);
                return Ok(products );
            }

            return BadRequest(response.ReasonPhrase);
        }
    }

    public class ProductsResponse
    {
        public required List<Product> Products { get; set; }
    }
}
