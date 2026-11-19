using Microsoft.AspNetCore.Mvc;

namespace VOIDLINE.Controllers;

[Route("login")]
public class LoginController : Controller
{
    public IActionResult Login()
    {
        return View();
    }
}
