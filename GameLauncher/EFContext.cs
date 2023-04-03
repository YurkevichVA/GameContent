using Microsoft.EntityFrameworkCore;

namespace GameLauncher
{
    public class EFContext: DbContext
    {
        public DbSet<Entities.Player> Players { get; set; }
        public DbSet<Entities.Content> Contents { get; set; }
        public DbSet<Entities.Transaction> Transactions { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .UseSqlServer(@"Data Source=(LocalDB)\MSSQLLocalDB;Initial Catalog=content_db;Integrated Security=True")
                .UseAllCheckConstraints();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Entities.Player>().HasIndex(p => p.Login).IsUnique(true);
            modelBuilder.Entity<Entities.Player>().HasIndex(p => p.Email).IsUnique(true);
        }
    }
}
