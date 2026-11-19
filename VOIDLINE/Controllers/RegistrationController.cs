using Microsoft.AspNetCore.Mvc;

namespace VOIDLINE.Controllers;

public class RegistrationController : Controller
{
    public IActionResult Registration()
    {
        return View();
    }
}
