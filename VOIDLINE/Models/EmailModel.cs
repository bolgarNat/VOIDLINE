using System.ComponentModel.DataAnnotations;

namespace VOIDLINE.Models;

public class EmailModel
{
    [Required(ErrorMessage = "Email is required")]
    [EmailAddress(ErrorMessage = "Must be a valid email address")]
    public string Email { get; set; } = "";
}
