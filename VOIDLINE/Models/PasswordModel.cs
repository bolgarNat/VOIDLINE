using System.ComponentModel.DataAnnotations;
using VOIDLINE.Attributes;

namespace VOIDLINE.Models;

public class PasswordModel
{
    [Required(ErrorMessage = "Password is required")]
    [ValidPassword(ErrorMessage = "Password must contain at least 8 characters and at least 1 digit")]
    public string Password {  get; set; }

    [Required(ErrorMessage = "Password is required")]
    [ValidPassword(ErrorMessage = "Password must contain at least 8 characters and at least 1 digit")]
    public string ConfirmPassword { get; set; }

    public PasswordModel(string password1, string password2)
    {
        Password = password1;
        ConfirmPassword = password2;
    }

    public PasswordModel()
    {
    }
}
