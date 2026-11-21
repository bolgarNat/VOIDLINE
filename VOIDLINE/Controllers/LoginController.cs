using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using VOIDLINE.Extensions;
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

        return Redirect("/");
    }

    [HttpGet("open-forgot-password")]
    public async Task<IActionResult> OpenForgetPassword()
    {
        TempData["ForgotPassword"] = true;

        return RedirectToAction(nameof(Login));
    }


    [HttpPost("forgot-password")]
    public async Task<IActionResult> ForgotPassword(EmailModel model)
    {
        TempData["ForgotPassword"] = true;

        if (model.Email != String.Empty)
        {
            HttpContext.Session.SetString(
                "EmailModel", JsonSerializer.Serialize(model));

            if (!ModelState.IsValid)
                return View("Login");
        }
        else
        {
            var json = HttpContext.Session.GetString("EmailModel");
            model = JsonSerializer.Deserialize<EmailModel>(json);
        }

        using (var context = new DBContext())
        {
            var user = context.Users.FirstOrDefault(x => model.Email == x.Email);

            if (user == null)
            {
                ViewData["EmailNotFound"] = "User not found";
                return RedirectToAction(nameof(Login));
            }
        }

        if (await _emailService.SendEmail(model.Email, null, null))
        {
            TempData["CodeVarification"] = true;
            TempData["ForgotPassword"] = false;

            TempData["ControllerName"] = "Login";
            TempData["ActionName"] = "ForgotPassword";
            TempData["ActionChangeEmail"] = "OpenForgetPassword";
            TempData["MethodChangeEmail"] = "get";
            TempData["ChangeEmailOnClick"] = "change_fp_modal_visability();change_cv_modal_visability()";

            TempData["Code"] = _emailService.GetMessageModel().GetCode();
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
            TempData["NewPassword"] = true;
        }
        else
        {
            TempData["CodeError"] = "Entered code is wrong";
            TempData["CodeVarification"] = true;
        }

        return RedirectToAction(nameof(Login));
    }

    [HttpPost("new-password")]
    public async Task<IActionResult> NewPassword(PasswordModel newPasswordModel)
    {
        TempData["NewPassword"] = true;

        if (!ModelState.IsValid)
            return View("Login");

        if (newPasswordModel.Password != newPasswordModel.ConfirmPassword)
        {
            ViewData["PasswordError"] = "Passwords do not match";
            return RedirectToAction(nameof(Login));
        }

        try
        {
            var json = HttpContext.Session.GetString("EmailModel");
            EmailModel? emailModel = JsonSerializer.Deserialize<EmailModel>(json);

            using DBContext context = new DBContext();

            var user = await context.Users.FirstOrDefaultAsync(x => emailModel.Email == x.Email);
            if (user is null)
            {
                ViewData["PasswordError"] = "User not found";
                return RedirectToAction(nameof(Login));
            }

            user.Password = _passwordService.PasswordConvert(newPasswordModel.Password);

            await context.SaveChangesAsync();

            TempData["NewPassword"] = false;
            return RedirectToAction(nameof(Login));
        }
        catch (Exception ex)
        {
            ViewData["PasswordError"] = "Cannot access database";
            return RedirectToAction(nameof(Login));
        }
    }
}
