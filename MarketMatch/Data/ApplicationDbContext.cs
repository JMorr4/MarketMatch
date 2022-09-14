using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace MarketMatch.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<MarketMatch.Models.Supermarket> Supermarket { get; set; } = default!;

        public DbSet<MarketMatch.Models.Product>? Product { get; set; }

        public DbSet<MarketMatch.Models.ProductPrice>? ProductPrice { get; set; }

        public DbSet<MarketMatch.Models.ShoppingList>? ShoppingList { get; set; }
    }
}