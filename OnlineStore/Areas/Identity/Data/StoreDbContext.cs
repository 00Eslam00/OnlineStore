using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using OnlineStore.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;
using OnlineStore.Models;
using System.Reflection.Emit;
using System.Reflection.Metadata;

namespace OnlineStore.Data;

public class StoreDbContext : IdentityDbContext<AppUser>
{
    public StoreDbContext(DbContextOptions<StoreDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<Cart>().HasKey(c => new { c.ProductId, c.UserId ,c.SellerId});
        base.OnModelCreating(builder);
        
    }
    public DbSet<Product> Products { get; set; }
    public DbSet<Cart> Carts { get; set; }
}
