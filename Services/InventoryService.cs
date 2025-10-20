using System.Text.Json;
using InventoryWeb.Models;

namespace InventoryWeb.Services
{
    public class InventoryService
    {
        private readonly string _filePath;
        private readonly object _lock = new();
        private List<Item> _items = new();
        private readonly ILogger<InventoryService>? _logger;

        // rootPath is expected to be ContentRootPath, pass via DI
        public InventoryService(IWebHostEnvironment env, ILogger<InventoryService> logger)
        {
            _logger = logger;
            _filePath = Path.Combine(env.ContentRootPath, "inventory.json");
            LoadItems();
        }

        public List<Item> GetAll()
        {
            lock (_lock)
            {
                return _items.ToList();
            }
        }

        public void AddItem(Item item)
        {
            lock (_lock)
            {
                _items.Add(item);
                SaveItems();
            }
        }

        private void LoadItems()
        {
            try
            {
                if (File.Exists(_filePath))
                {
                    var json = File.ReadAllText(_filePath);
                    var deserialized = JsonSerializer.Deserialize<List<Item>>(json);
                    _items = deserialized ?? new List<Item>();
                }
                else
                {
                    _items = new List<Item>();
                }
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "Failed to load items from {File}", _filePath);
                _items = new List<Item>();
            }
        }

        private void SaveItems()
        {
            try
            {
                var json = JsonSerializer.Serialize(_items, new JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText(_filePath, json);
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "Failed to save items to {File}", _filePath);
            }
        }
    }
}
