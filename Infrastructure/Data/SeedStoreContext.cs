using System.Text.Json;
using Core;

namespace Infrastructure;

public class SeedStoreContext
{
  public static async Task SeedDatabase(StoreContext context)
  {
    if (!context.Products.Any())
    {
      Console.WriteLine("***************************SEEDING**********************************");
      var productsData = await File.ReadAllTextAsync("../Infrastructure/Data/Seeds/products.json");

      var products = JsonSerializer.Deserialize<List<Product>>(productsData);

      if (products == null)
      {
        return;
      }

      context.Products.AddRange(products);

      await context.SaveChangesAsync();
    }
  }
}