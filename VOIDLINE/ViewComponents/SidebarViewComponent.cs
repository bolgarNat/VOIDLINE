using Microsoft.AspNetCore.Mvc;
using VOIDLINE.Models.Database;
using VOIDLINE.Services;

namespace VOIDLINE.ViewComponents;

public class SidebarViewComponent : ViewComponent
{
    private readonly DatabaseService<Categoryvariant> _categoryService;

    public SidebarViewComponent(DatabaseService<Categoryvariant> categoryService)
    {
        _categoryService = categoryService;
    }

    public async Task<IViewComponentResult> InvokeAsync()
    {
        await _categoryService.FillForeignTables();

        var categoryServiceData = await _categoryService.ReadFunc();
        if (categoryServiceData != null)
        {
            ViewData["Category"] = categoryServiceData;
        }
        return View();
    }
}
