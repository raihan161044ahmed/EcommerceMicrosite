using System.ComponentModel.DataAnnotations;

namespace Ecommerce.API.DTOs.Auth
{
    public class LoginRequest
    {
        [Required, MinLength(3)] public string Username { get; set; } = default!;
        [Required, MinLength(6)] public string Password { get; set; } = default!;
    }
}
