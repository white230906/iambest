using Microsoft.EntityFrameworkCore;
using TestRepo.Repository.Entity;

namespace TestRepo.Repository;

public class AppDbContext : DbContext
{
    
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options) { }

    public DbSet<User> Users { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Seller> Sellers { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>(builder =>
        {
            var users = new List<User>()
            {
                new User()
                {
                    Id = Guid.NewGuid(),
                    Password = "PiedTeam",
                    Email = "admin@gmail.com",
                    Role = "Admin"
                }
            };
            builder.HasData(users);
        });

    }
}