using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using VOIDLINE.Models;

namespace VOIDLINE.Controllers;

public class HomeController : Controller
{
    private readonly DBContext _context = new DBContext();

    public async Task<IActionResult> Index()
    {
        var variant = await _context.Categoryvariants
            .Include(v => v.Categories)
            .ToListAsync();

        var categoryVariants = await _context.Categoryvariants
            .ToListAsync();

        return View(categoryVariants);
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
