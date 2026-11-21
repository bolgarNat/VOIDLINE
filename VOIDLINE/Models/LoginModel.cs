using System.ComponentModel.DataAnnotations;
using VOIDLINE.Attributes;

namespace VOIDLINE.Models;

public class LoginModel
{
    [Required(ErrorMessage = "Email is required")]
    [EmailAddress(ErrorMessage = "Must be a valid email address")]
    public string Email { get; set; } = "";

    [Required(ErrorMessage = "Password is required")]
    [ValidPassword(ErrorMessage = "Password must contain at least 8 characters and at least 1 digit")]
    public string Password { get; set; } = "";
}
