using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using VOIDLINE.Models;
using VOIDLINE.Models.Database;
using VOIDLINE.Services;

namespace VOIDLINE.Controllers;

public class HomeController : Controller
{
    private readonly DatabaseService<Categoryvariant> _categoryService;

    private readonly DatabaseService<Product> _productService;

    public HomeController(DatabaseService<Categoryvariant> categoryService, DatabaseService<Product> productService)
    {
        _categoryService = categoryService;
        _productService = productService;
    }

    public async Task<IActionResult> Home()
    {
        await _categoryService.FillForeignTables();

        var categoryServiceData = await _categoryService.ReadFunc();
        if (categoryServiceData != null)
        {
            ViewData["Category"] = categoryServiceData;
        }

        var productServiceData = await _productService.ReadFunc();
        if (productServiceData != null)
        {
            ViewData["Product"] = productServiceData;
        }

        return View();
    }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
