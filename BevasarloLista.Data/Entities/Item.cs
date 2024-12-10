using System.ComponentModel.DataAnnotations;

namespace BevasarloLista.Data.Entities
{
    public class Item
    {
        [Key]
        public int Id { get; set; } // This will be auto-incremented in the database
        public string Name { get; set; }
        public double Amount { get; set; }
        public double Price { get; set; }
        public DateTime PurchaseDate { get; set; }
        public int? ForUserId { get; set; } // UserId who the item is for
        public int? CheckedById { get; set; } // UserId who checked the item
    }
}
