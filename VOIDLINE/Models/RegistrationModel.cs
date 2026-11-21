using System.ComponentModel.DataAnnotations;
using VOIDLINE.Attributes;

namespace VOIDLINE.Models;

public class RegistrationModel
{
    [Required(ErrorMessage = "Name is required")]
    [NameAttribute(ErrorMessage = "Name must not contain digits")]
    public string Name { get; set; } = "";


    [Required(ErrorMessage = "Surname is required")]
    [NameAttribute(ErrorMessage = "Surname must not contain digits")]
    public string Surname { get; set; } = "";


    [Required(ErrorMessage = "Email is required")]
    [EmailAddress(ErrorMessage = "Must be a valid email address")]
    public string Email { get; set; } = "";


    [Required(ErrorMessage = "Password is required")]
    [ValidPassword(ErrorMessage = "Password must contain at least 8 characters and at least 1 digit")]
    public string Password { get; set; } = "";
}
