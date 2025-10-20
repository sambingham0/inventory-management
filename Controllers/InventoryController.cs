using Microsoft.AspNetCore.Mvc;
using InventoryWeb.Models;
using InventoryWeb.Services;

namespace InventoryWeb.Controllers
{
    public class InventoryController : Controller
    {
        private readonly InventoryService _inventoryService;

        public InventoryController(InventoryService inventoryService)
        {
            _inventoryService = inventoryService;
        }

        // GET: /Inventory
        public IActionResult Index(string? category, string? sortBy)
        {
            var items = _inventoryService.GetAll();

            // Optional filtering by category
            if (!string.IsNullOrWhiteSpace(category))
            {
                var cat = category.Trim();
                items = items
                    .Where(i => string.Equals(i.Category, cat, StringComparison.OrdinalIgnoreCase))
                    .ToList();
            }

            // Optional sorting
            if (!string.IsNullOrWhiteSpace(sortBy))
            {
                var sort = sortBy.Trim().ToLowerInvariant();
                items = sort switch
                {
                    "name" => items.OrderBy(i => i.Name, StringComparer.OrdinalIgnoreCase).ToList(),
                    "quantity" => items.OrderBy(i => i.Quantity).ToList(),
                    "price" => items.OrderBy(i => i.Price).ToList(),
                    "category" => items.OrderBy(i => i.Category, StringComparer.OrdinalIgnoreCase).ToList(),
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
        [ValidateAntiForgeryToken]
        public IActionResult Add(Item item)
        {
            if (ModelState.IsValid)
            {
                _inventoryService.AddItem(item);
                TempData["Message"] = $"Added {item.Name}.";
                return RedirectToAction("Index");
            }
            return View(item);
        }
    }
}
