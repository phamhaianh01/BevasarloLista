using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BevasarloLista.Data.Entities
{
    public class Item
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Amount { get; set; }
        public double Price { get; set; }
        public DateTime PurchaseDate { get; set; }
        public User? For { get; set; } // User who the item is for
        public User? CheckedBy { get; set; } // User who checked the item

    }
}
