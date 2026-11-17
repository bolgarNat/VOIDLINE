using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using VOIDLINE.Models;
using VOIDLINE.Services;

namespace VOIDLINE.Controllers;

public class HomeController : Controller
{
    private readonly DatabaseService<Categoryvariant> _categoryService;

    public HomeController(DatabaseService<Categoryvariant> categoryService)
    {
        _categoryService = categoryService;
    }

    public async Task<IActionResult> Index()
    {
        await _categoryService.FillForeignTables();
        var categories = await _categoryService.ReadFunc();

        return View(categories);
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
