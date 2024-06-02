using System.ComponentModel.DataAnnotations;

namespace OrderService.Domain.Aggregates.ProfileAggregate.DTO.Request;

public class SignUpDTO
{
    [Required(ErrorMessage = "Username is required.")]
    [EmailAddress(ErrorMessage = "Username must be a valid email address.")]
    public string Username { get; set; }

    [Required(ErrorMessage = "Password is required.")]
    [StringLength(100, MinimumLength = 6, ErrorMessage = "Password must be at least 6 characters long.")]
    public string Password { get; set; }

    [Required(ErrorMessage = "Confirm Password is required.")]
    [Compare("Password", ErrorMessage = "Password and Confirm Password do not match.")]
    public string ConfirmPassword { get; set; }
}
