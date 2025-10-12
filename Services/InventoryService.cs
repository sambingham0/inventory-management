using System.Text.Json;
using InventoryWeb.Models;

namespace InventoryWeb.Services
{
    public class InventoryService
    {
        private readonly string _filePath = "inventory.json";
        private List<Item> _items;

        public InventoryService()
        {
            LoadItems();
        }

        public List<Item> GetAll() => _items;

        public void AddItem(Item item)
        {
            _items.Add(item);
            SaveItems();
        }

        private void LoadItems()
        {
            if (File.Exists(_filePath))
            {
                var json = File.ReadAllText(_filePath);
                _items = JsonSerializer.Deserialize<List<Item>>(json) ?? new List<Item>();
            }
            else
            {
                _items = new List<Item>();
            }
        }

        private void SaveItems()
        {
            var json = JsonSerializer.Serialize(_items, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(_filePath, json);
        }
    }
}
