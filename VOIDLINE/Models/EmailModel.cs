using System.ComponentModel.DataAnnotations;

namespace VOIDLINE.Models;

public class EmailModel
{
    [Required]
    private string EmailAddress { get; set; }

    [Required]
    private string EmailPasswd { get; set; }
}
