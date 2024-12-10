using System.ComponentModel.DataAnnotations;

namespace BevasarloLista.Data.Entities
{
    public class User
    {
        [Key]
        public  int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; } = string.Empty;
    }
}
