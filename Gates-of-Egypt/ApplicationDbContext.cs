using Gates_of_Egypt.Models;
using Microsoft.EntityFrameworkCore;

namespace Gates_of_Egypt
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductTranslation> ProductTranslations { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<CartItem> CartItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // One-to-One: User <-> Cart
            modelBuilder.Entity<User>()
                .HasOne(u => u.Cart)
                .WithOne(c => c.User)
                .HasForeignKey<Cart>(c => c.UserId);

            // One-to-Many: Product <-> ProductTranslations
            modelBuilder.Entity<ProductTranslation>()
                .HasOne(pt => pt.Product)
                .WithMany(p => p.Translations)
                .HasForeignKey(pt => pt.ProductId);

            // One-to-Many: Cart <-> CartItems
            modelBuilder.Entity<CartItem>()
                .HasOne(ci => ci.Cart)
                .WithMany(c => c.CartItems)
                .HasForeignKey(ci => ci.CartId);

            // One-to-Many: CartItem <-> Product
            modelBuilder.Entity<CartItem>()
                .HasOne(ci => ci.Product)
                .WithMany()
                .HasForeignKey(ci => ci.ProductId);
        }
    }
}
