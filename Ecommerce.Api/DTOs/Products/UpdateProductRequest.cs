using System.ComponentModel.DataAnnotations;

namespace Ecommerce.API.DTOs.Products
{
    public class UpdateProductRequest
    {
        [Required] public Guid Id { get; set; }
        [Required, MaxLength(200)] public string Name { get; set; } = default!;
        [MaxLength(4000)] public string? Description { get; set; }
        [Range(0.0, double.MaxValue)] public decimal Price { get; set; }
        [Range(0, int.MaxValue)] public int StockQuantity { get; set; }
    }
}
