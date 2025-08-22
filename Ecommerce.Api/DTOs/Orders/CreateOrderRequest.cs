using System.ComponentModel.DataAnnotations;

namespace Ecommerce.API.DTOs.Orders
{
    public class CreateOrderRequest
    {
        [Required] public Guid CustomerId { get; set; }
        [Required] public string ShippingStreet { get; set; } = default!;
        [Required] public string ShippingCity { get; set; } = default!;
        [Required] public string ShippingState { get; set; } = default!;
        [Required] public string ShippingZipCode { get; set; } = default!;
        [Required, MinLength(1)] public List<OrderItemRequest> Items { get; set; } = new();
    }
}
