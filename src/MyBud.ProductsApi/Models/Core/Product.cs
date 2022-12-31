using System.ComponentModel.DataAnnotations;

namespace MyBud.ProductsApi.Models.Core
{
    public class Product
    {
        [Key]
        public int ProductId { get; set; }
        public Guid UserId { get; set; }
        public string Name { get; set; }
        public ProductCategory Category { get; set; }
        public double Price { get; set; }
        public double SalePrice { get; set; }
        public string? Image { get; set; }
        public string? ImageUrl { get; set; }
        public int Quantity { get; set; }
    }
}
