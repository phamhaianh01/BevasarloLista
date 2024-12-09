using Microsoft.EntityFrameworkCore;

namespace BevasarloLista.Data.Entities
{
    public class ListDbContext : DbContext
    {
        public ListDbContext(DbContextOptions<ListDbContext> options) : base(options)
        {

        }

        public DbSet<Item> Items => Set<Item>();

        public DbSet<User> Users => Set<User>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Item>(item =>
            {
                item.Property(i => i.Name)
                    .HasMaxLength(100);
                item.HasIndex(i => i.Name);
            });


            modelBuilder.Entity<User>(user =>
            {
                user.Property(u => u.Username).HasMaxLength(50);
                user.HasIndex(u => u.Username).IsUnique();
            });
        }
    }
}
