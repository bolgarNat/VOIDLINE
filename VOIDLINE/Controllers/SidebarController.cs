using Microsoft.AspNetCore.Mvc;
using VOIDLINE.Models.Database;

namespace VOIDLINE.Controllers;

public class SidebarController : Controller
{

};

public static List<Category> Derr()
{
    using (DBContext dbContext = new DBContext())
    {
        return dbContext.Categories.ToList();
    }
}