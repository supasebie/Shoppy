using Core;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure;
public class StoreContext(DbContextOptions options) : DbContext(options)
{
  public DbSet<Product> Products { get; set; }
  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    base.OnModelCreating(modelBuilder);
    modelBuilder.ApplyConfigurationsFromAssembly(typeof(ProductConfig).Assembly);
  }
}