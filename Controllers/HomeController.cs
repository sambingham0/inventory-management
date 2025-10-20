using Microsoft.AspNetCore.Mvc;

namespace InventoryWeb.Controllers;

public class HomeController : Controller
{
    public IActionResult Index()
    {
        // Redirect Home to Inventory list
        return RedirectToAction("Index", "Inventory");
    }
}
