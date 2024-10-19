namespace Core;

public interface IProductRepo
{
  Task<IReadOnlyList<Product>> GetProductsAsync(string? brand, string? type, string? sort);
  Task<Product?> GetProductAsync(int id);
  Task<IReadOnlyList<string>> GetBrandsAsync();
  Task<IReadOnlyList<string>> GetTypesAsync();
  void UpdateProduct(Product product);
  void AddProduct(Product product);
  void DeleteProduct(Product product);
  bool ProductExists(int id);
  Task<bool> SaveChangesAsync();
}