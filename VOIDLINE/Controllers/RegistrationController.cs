using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using VOIDLINE.Models;
using VOIDLINE.Models.Database;
using VOIDLINE.Services;

namespace VOIDLINE.Controllers;

[Route("registration")]
public class RegistrationController : Controller
{
    private readonly PasswordService _passwordService;

    private readonly LoggerService _loggerService;

    private readonly EmailService _emailService;

    public RegistrationController(PasswordService passwordService, 
                                  EmailService emailService,
                                  LoggerService loggerService)
    {
        _passwordService = passwordService;
        _emailService = emailService;
        _loggerService = loggerService;
    }

    [HttpGet("")]
    public IActionResult Registration()
    {
        var json = HttpContext.Session.GetString("RegistrationModel");

        if (json != null)
        {
            var model = JsonSerializer.Deserialize<RegistrationModel>(json);
            return View(model);
        }

        return View(new RegistrationModel());
    }

    [HttpGet("change-email")]
    public async Task<IActionResult> ChangeEmail()
    {
        TempData["CodeVarification"] = false;

        return RedirectToAction(nameof(Registration));
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

        HttpContext.Session.SetString(
            "RegistrationModel",
            JsonSerializer.Serialize(model));

        var emailModel = new EmailModel();
        emailModel.Email = model.Email;

        await SendEmail(emailModel); 

        return RedirectToAction(nameof(Registration), model);
    }

    [HttpPost("send-email")]
    public async Task<IActionResult> SendEmail(EmailModel emailModel)
    {
        RegistrationModel? registrationModel = new();

        var json = HttpContext.Session.GetString("RegistrationModel");
        if (json != null)
        {
            registrationModel = JsonSerializer.Deserialize<RegistrationModel>(json);
        }

        if (emailModel.Email == String.Empty)
        {
            emailModel.Email = registrationModel.Email;
        }

        if (!ModelState.IsValid)
        {
            // че-то
        }

        if (await _emailService.SendEmail(emailModel.Email, null, null))
        {
            TempData["CodeVarification"] = true;
            TempData["ControllerName"] = "Registration";
            TempData["ActionName"] = "SendEmail";
            TempData["ActionChangeEmail"] = "ChangeEmail";
            TempData["MethodChangeEmail"] = "get";

            TempData["ChangeEmailOnClick"] = "change_cv_modal_visability()";

            TempData["Code"] = _emailService.GetMessageModel().GetCode();
        }
        else
        {
            ViewData["SendEmailError"] = "Cannot send an email";
        }

        return RedirectToAction(nameof(Registration), registrationModel);
    }

    [HttpPost("enter-code")]
    public async Task<IActionResult> CodeVarification(string digit1, string digit2, string digit3, string digit4)
    {
        string code = digit1 + digit2 + digit3 + digit4;

        RegistrationModel? registrationModel = new();
        var json = HttpContext.Session.GetString("RegistrationModel");
        if (json != null)
        {
            registrationModel = JsonSerializer.Deserialize<RegistrationModel>(json);
        }

        if (code == TempData["Code"]!.ToString())
        {
            string errorMessage = await CreateUser(registrationModel);

            if (errorMessage != String.Empty)
            {
                TempData["RegisterError"] = "Cannot access database";
                return RedirectToAction(nameof(Registration), registrationModel);
            }

            return Redirect("/");
        }
        else
        {
            TempData["CodeError"] = "Entered code is wrong";
            TempData["CodeVarification"] = true;
        }

        return RedirectToAction(nameof(Registration), registrationModel);
    }

    private async Task<string> CreateUser(RegistrationModel registrationModel)
    {
        try
        {
            using var context = new DBContext();

            User user = new User { Name = registrationModel.Name, 
                                   Surname = registrationModel.Surname, 
                                   Email = registrationModel.Email, 
                                   Password = _passwordService.PasswordConvert(registrationModel.Password) };

            context.Users.Add(user);

            await context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            string exStr = ex.ToString();
            _loggerService.LogError(exStr);

            return exStr;
        }

        return "";
    }
}
