namespace Core;

public class ProductSpec : BaseSpecification<Product>
{
  public ProductSpec(string? brand, string? type, string? sort) : base(x =>
  (string.IsNullOrWhiteSpace(brand) || x.Brand == brand) &&
  (string.IsNullOrWhiteSpace(type) || x.Type == type)
  )

  {
    switch (sort)
    {
      case "priceAsc":
        AddOrderBy(x => x.Price);
        break;
      case "priceDesc":
        AddOrderByDesc(x => x.Price);
        break;
      default:
        AddOrderBy(x => x.Name);
        break;
    }
  }
}