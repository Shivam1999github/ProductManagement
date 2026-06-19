using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Product> Product => Set<Product>();

        public DbSet<Item> Item => Set<Item>();
        public DbSet<ApplicationUser> ApplicationUser => Set<ApplicationUser>();

        public DbSet<RefreshToken> RefreshToken => Set<RefreshToken>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>()
                .HasMany(x => x.Item)
                .WithOne(x => x.Product)
                .HasForeignKey(x => x.ProductId);

            base.OnModelCreating(modelBuilder);
        }
    }
}
