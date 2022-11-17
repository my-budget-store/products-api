using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using MyBud.ProductsApi.Interfaces;
using MyBud.ProductsApi.Models.Core;
using System.Net;

namespace MyBud.ProductsApi.Controllers.V1
{
    [ApiController]
    [ApiVersion("1.0")]
    [Authorize]
    [Route("v{version:apiVersion}/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductsRepository _productsRepository;

        public ProductsController(IProductsRepository productsRepository)
        {
            _productsRepository = productsRepository;
        }

        /// <summary>
        /// Get list of all products
        /// </summary>
        /// <returns>List of all products</returns>
        [ProducesResponseType(typeof(IEnumerable<Product>), (int)HttpStatusCode.OK)]
        [HttpGet]
        public async Task<IActionResult> GetProducts()
        {
            var products = await _productsRepository.GetProducts();

            return Ok(products);
        }

        /// <summary>
        /// Get details of a specific product
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Details of a specific product</returns>
        [ProducesResponseType(typeof(Product), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductById(int id)
        {
            var product = await _productsRepository.GetProductById(id);

            return product != null
                ? Ok(product)
                : NotFound();
        }

        /// <summary>
        /// Search products
        /// </summary>
        /// <param name="value"></param>
        /// <returns>List of matching products</returns>
        [ProducesResponseType(typeof(IEnumerable<Product>), (int)HttpStatusCode.OK)]
        [HttpGet("Search")]
        public async Task<IActionResult> SearchProducts(string value)
        {
            var products = await _productsRepository.SearchProducts(value);

            return products != null
                ? Ok(products)
                : NotFound();
        }

        /// <summary>
        /// Create product 
        /// </summary>
        /// <param name="product"></param>
        /// <returns>Result of product creation</returns>
        [ProducesResponseType((int)HttpStatusCode.Created)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [HttpPost]
        [ProducesResponseType(typeof(IDictionary<string, string>), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> CreateProduct(Product product)
        {
            //TODO: validate model
            var createdProduct = await _productsRepository.CreateProduct(product);

            return createdProduct != null
                ? Created(new Uri(Request.GetEncodedUrl()), createdProduct)
                : BadRequest();
        }

        /// <summary>
        /// Update specific product
        /// </summary>
        /// <param name="product"></param>
        /// <returns>Updated product</returns>
        [HttpPut]
        [ProducesResponseType(typeof(Product), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> UpdateProduct(Product product)
        {
            //ToDo: Validate input model
            var isExistingProduct = await _productsRepository.GetProductById(product.ProductId) != null;

            if (isExistingProduct)
            {
                var updatedProduct = await _productsRepository.UpdateProduct(product);

                return Ok(updatedProduct);
            }

            return NotFound(product);
        }

        /// <summary>
        /// Delete specific product
        /// </summary>
        /// <param name="productId"></param>
        /// <returns>Result of product deletion</returns>
        [ProducesResponseType(typeof(Product), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int productId)
        {
            var product = await _productsRepository.GetProductById(productId);
            if (product == null)
            {
                return NotFound();
            }

            var isDeleted = await _productsRepository.DeleteProduct(product);

            return isDeleted
                ? Ok()
                : NotFound();
        }
    }
}