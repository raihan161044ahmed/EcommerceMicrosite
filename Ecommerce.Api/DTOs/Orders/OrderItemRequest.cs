using System.ComponentModel.DataAnnotations;

namespace Ecommerce.API.DTOs.Orders
{
    public class OrderItemRequest
    {
        [Required] public Guid ProductId { get; set; }
        [Range(1, int.MaxValue)] public int Quantity { get; set; }
        [Range(0.0, double.MaxValue)] public decimal UnitPrice { get; set; }
    }
}
