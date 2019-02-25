using Microsoft.EntityFrameworkCore;
using qoden_chat.src.Database.Entities;

namespace qoden_chat.src.Database
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options)
            : base (options){}
        
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasIndex(u => u.Username).IsUnique();
            base.OnModelCreating(modelBuilder);

            var pass = PasswordGenerator.HashPassword("123");

            modelBuilder.Entity<User>().HasData(new User
            {
                Id = 100,
                Username = "Tolstovku",
                Password = pass
            },
                new User
            {
                Id = 200,
                Username = "Slimakanzer",
                Password = pass
            },new User
            {
                Id = 300,
                Username = "Nimatora",
                Password = pass
            }
                );
        }
    }
}