using Core;
using Microsoft.AspNetCore.Mvc;

namespace Api;

[ApiController]
[Route("api/[controller]")]
public class ProductsController(IGenericRepo<Product> repo) : ControllerBase
{
  [HttpGet]
  public async Task<ActionResult<IReadOnlyList<Product>>> GetProductsAsync(string? brand, string? type, string? sort)
  {
    var spec = new ProductSpec(brand, type, sort);

    var products = await repo.GetAllWithSpec(spec);

    return Ok(products);
  }

  [HttpGet("{id:int}")] // api/products/2
  public async Task<ActionResult<Product>> GetProduct(int id)
  {
    var product = await repo.GetByIdAsync(id);

    if (product == null) return NotFound();

    return product;
  }

  [HttpPost]
  public async Task<ActionResult<Product>> CreateProduct(Product product)
  {
    repo.Add(product);

    if (await repo.SaveChangesAsync())
    {
      return CreatedAtAction("GetProduct", new { id = product.Id }, product);
    }

    return BadRequest("Problem creating product");
  }

  [HttpPut("{id:int}")]
  public async Task<ActionResult> UpdateProduct(int id, Product product)
  {
    if (product.Id != id || !repo.Exists(id))
    {
      return BadRequest("Cannot update this product");
    }

    repo.Update(product);

    if (await repo.SaveChangesAsync())
    {
      return NoContent();
    }

    return BadRequest("Problem updating product");
  }

  [HttpDelete("{id:int}")]
  public async Task<ActionResult> DeleteProduct(int id)
  {
    var product = await repo.GetByIdAsync(id);

    if (product == null)
    {
      return NotFound();
    }

    repo.Delete(product);

    if (await repo.SaveChangesAsync())
    {
      return NoContent();
    }

    return BadRequest("Problem deleting product");
  }

  // [HttpGet("brands")]
  // public async Task<ActionResult<IReadOnlyList<string>>> GetBrands()
  // {
  //   return Ok(await repo.GetBrandsAsync());
  // }

  // [HttpGet("types")]
  // public async Task<ActionResult<IReadOnlyList<string>>> GetTypes()
  // {
  //   return Ok(await repo.GetTypesAsync());
  // }
}