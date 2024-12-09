namespace BevasarloLista.Data.Entities
{
    public class Item
    {
        public int Id { get; set; } // This will be auto-incremented in the database
        public string Name { get; set; }
        public int Amount { get; set; }
        public double Price { get; set; }
        public DateTime PurchaseDate { get; set; }
        public int? ForId { get; set; } // UserId who the item is for
        public int? CheckedById { get; set; } // UserId who checked the item
    }
}
