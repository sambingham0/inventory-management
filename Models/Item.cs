namespace InventoryWeb.Models
{
    public class Item
    {
        public string Name { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public string Category { get; set; }

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
            return $"({Category}) {Name} - Quantity: {Quantity}, Price: ${Price}";
        }
    }
}