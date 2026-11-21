using Microsoft.AspNetCore.Mvc;
using VOIDLINE.Models;
using VOIDLINE.Models.Database;
using VOIDLINE.Services;

namespace VOIDLINE.Controllers;

[Route("login")]
public class LoginController : Controller
{
    private readonly PasswordService _passwordService;

    private readonly EmailService _emailService;

    public LoginController(PasswordService passwordService, EmailService emailService)
    {
        _passwordService = passwordService;
        _emailService = emailService;
    }

    [HttpGet("")]
    public IActionResult Login()
    {
        return View(new LoginModel());
    }

    [HttpPost("")]
    public async Task<IActionResult> LogIn(LoginModel model)
    {
        if (!ModelState.IsValid)
            return View(model);

        using (var context = new DBContext())
        {
            var user = context.Users.FirstOrDefault(x => model.Email == x.Email && _passwordService.PasswordConvert(model.Password) == x.Password);
            
            if (user == null)
            {
                ViewData["LoginError"] = "User not found";
                return View(model);
            }
        }

        return RedirectToAction(nameof(Login));
    }

    [HttpPost("forgot-password")]
    public async Task<IActionResult> ForgotPassword(string email)
    {
        if (email != null)
        {
            TempData["Email"] = email;
        }

        if (await _emailService.SendEmail(TempData["Email"]!.ToString()!, null, null))
        {
            TempData["CodeVarification"] = true;
            TempData["ControllerName"] = "Login";
            TempData["ActionName"] = "ForgotPassword";
            TempData["ChangeEmailOnClick"] = "change_fp_modal_visability();change_cv_modal_visability()";

            TempData["Code"] = _emailService.GetCode();
        }
        else
        {
            ViewData["SendEmailError"] = "Cannot send an email";
        }

        return RedirectToAction(nameof(Login));
    }


    [HttpPost("enter-code")]
    public async Task<IActionResult> CodeVarification(string digit1, string digit2, string digit3, string digit4)
    {
        string code = digit1 + digit2 + digit3 + digit4;

        if (code == TempData["Code"]!.ToString())
        {
            return Redirect("/");
        }
        else
        {
            TempData["CodeError"] = "Entered code is wrong";
            TempData["CodeVarification"] = true;
        }

        return RedirectToAction(nameof(Login));
    }
}
