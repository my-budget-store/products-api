using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using MyBud.ProductsApi.Interfaces;
using MyBud.ProductsApi.Models.Core;

namespace MyBud.ProductsApi.Controllers.V1
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("v{version:apiVersion}/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductsService _productsService;

        public ProductsController(IProductsService productsService)
        {
            _productsService = productsService;
        }

        [HttpGet]
        public async Task<IActionResult> GetProducts()
        {
            var products = await _productsService.GetProducts();

            return Ok(products);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductById(int id)
        {
            var product = await _productsService.GetProductById(id);

            return product != null
                ? Ok(product)
                : NotFound();
        }

        [HttpGet("Search")]
        public async Task<IActionResult> SearchProducts(string value)
        {
            var products = await _productsService.SearchProducts(value);

            return products != null
                ? Ok(products)
                : NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct(Product product)
        {
            var createdProduct = await _productsService.CreateProduct(product);

            return createdProduct != null
                ? Created(new Uri(Request.GetEncodedUrl()), createdProduct)
                : BadRequest();
        }

        [HttpPut]
        public async Task<IActionResult> UpdateProduct(Product product)
        {
            var updatedProduct = await _productsService.UpdateProduct(product);

            return Ok(updatedProduct);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int productId)
        {
            var product = await _productsService.GetProductById(productId);
            if (product == null)
            {
                return NotFound();
            }

            var isDeleted = await _productsService.DeleteProduct(product);

            return isDeleted
                ? Ok()
                : NotFound();
        }
    }
}