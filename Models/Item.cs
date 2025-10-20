using System.ComponentModel.DataAnnotations;

namespace InventoryWeb.Models
{
    public class Item
    {
        [Required]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;

        [Range(0, int.MaxValue)]
        public int Quantity { get; set; }

        [Range(0, double.MaxValue)]
        [DataType(DataType.Currency)]
        public decimal Price { get; set; }

        [Required]
        [StringLength(100)]
        public string Category { get; set; } = string.Empty;

        public Item() { }
    
        public Item(string name, int quantity, decimal price, string category)
        {
            Name = name;
            Quantity = quantity;
            Price = price;
            Category = category;
        }
    
        public override string ToString()
        {
            return $"({Category}) {Name} - Quantity: {Quantity}, Price: {Price:C}";
        }
    }
}