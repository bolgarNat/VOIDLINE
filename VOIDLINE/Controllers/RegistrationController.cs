using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Internal;
using VOIDLINE.Models;
using VOIDLINE.Models.Database;
using VOIDLINE.Services;

namespace VOIDLINE.Controllers;

[Route("registration")]
public class RegistrationController : Controller
{
    private readonly PasswordService _passwordService;

    private readonly EmailService _emailService;

    public RegistrationController(PasswordService passwordService, EmailService emailService)
    {
        _passwordService = passwordService;
        _emailService = emailService;
    }

    [HttpGet("")]
    public IActionResult Registration()
    {
        return View(new RegistrationModel());
    }

    [HttpPost("")]
    public async Task<IActionResult> Register(RegistrationModel model)
    {
        if (!ModelState.IsValid)
            return View("Registration", model);

        using (var context = new DBContext())
        {
            var user = context.Users.FirstOrDefault(x => model.Email == x.Email);

            if (user != null)
            {
                ViewData["RegisterError"] = "This email is already taken";
                return View("Registration", model);
            }
        }

        TempData["Email"] = model.Email;
        TempData["Model"] = model;

        // Send an email
        await SendEmail(model.Email);

        return RedirectToAction(nameof(Registration));
    }

    [HttpPost("send-email")]
    public async Task<IActionResult> SendEmail(string email)
    {
        if (await _emailService.SendEmail(email!.ToString()!, null, null))
        {
            TempData["CodeVarification"] = true;
            TempData["ControllerName"] = "Registration";
            TempData["ActionName"] = "SendEmail";
            TempData["ChangeEmailOnClick"] = "change_cv_modal_visability()";

            TempData["Code"] = _emailService.GetCode();
        }
        else
        {
            ViewData["SendEmailError"] = "Cannot send an email";
        }

        return RedirectToAction(nameof(Registration));
    }

    [HttpPost("enter-code")]
    public async Task<IActionResult> CodeVarification(string digit1, string digit2, string digit3, string digit4)
    {
        string code = digit1 + digit2 + digit3 + digit4;

        if (code == TempData["Code"]!.ToString())
        {
            // сюда нужно сохранение в БД -> для этого нужно сохранить где-то model
            return Redirect("/");
        }
        else
        {
            TempData["CodeError"] = "Entered code is wrong";
            TempData["CodeVarification"] = true;
        }

        return RedirectToAction(nameof(Registration));
    }
}
