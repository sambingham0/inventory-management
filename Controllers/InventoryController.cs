using Microsoft.AspNetCore.Mvc;
using InventoryWeb.Models;
using InventoryWeb.Services;

namespace InventoryWeb.Controllers
{
    public class InventoryController : Controller
    {
        private readonly InventoryService _inventoryService;

        public InventoryController()
        {
            _inventoryService = new InventoryService();
        }

        // GET: /Inventory
        public IActionResult Index(string category = null, string sortBy = null)
        {
            var items = _inventoryService.GetAll();

            // Optional filtering by category
            if (!string.IsNullOrEmpty(category))
            {
                items = items
                    .Where(i => i.Category.Equals(category, StringComparison.OrdinalIgnoreCase))
                    .ToList();
            }

            // Optional sorting
            if (!string.IsNullOrEmpty(sortBy))
            {
                items = sortBy.ToLower() switch
                {
                    "name" => items.OrderBy(i => i.Name).ToList(),
                    "quantity" => items.OrderBy(i => i.Quantity).ToList(),
                    "price" => items.OrderBy(i => i.Price).ToList(),
                    "category" => items.OrderBy(i => i.Category).ToList(),
                    _ => items
                };
            }

            return View(items);
        }

        // GET: /Inventory/Add
        public IActionResult Add()
        {
            return View();
        }

        // POST: /Inventory/Add
        [HttpPost]
        public IActionResult Add(Item item)
        {
            if (ModelState.IsValid)
            {
                _inventoryService.AddItem(item);
                return RedirectToAction("Index");
            }
            return View(item);
        }
    }
}
