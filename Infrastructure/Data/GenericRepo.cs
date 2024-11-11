using Core;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure;

public class GenericRepo<T>(StoreContext context) : IGenericRepo<T> where T : EntityBase
{
  public void Add(T entity)
  {
    context.Set<T>().Add(entity);
  }

  public void Delete(T entity)
  {
    context.Set<T>().Remove(entity);
  }

  public bool Exists(int id)
  {
    return context.Set<T>().Any(x => x.Id == id);
  }

  public async Task<IReadOnlyList<T>> GetAllAsync()
  {
    return await context.Set<T>().ToListAsync();
  }

  public async Task<T?> GetByIdAsync(int id)
  {
    return await context.Set<T>().FindAsync(id);
  }

  public async Task<bool> SaveChangesAsync()
  {
    return await context.SaveChangesAsync() > 0;
  }

  public void Update(T entity)
  {
    context.Set<T>().Attach(entity);
    context.Entry(entity).State = EntityState.Modified;
  }

  public async Task<T?> GetEntityWithSpec(ISpecification<T> spec)
  {
    return await ApplySpec(spec).FirstOrDefaultAsync();
  }

  public async Task<IReadOnlyList<T>> GetAllWithSpec(ISpecification<T> spec)
  {
    return await ApplySpec(spec).ToListAsync();
  }

  private IQueryable<T> ApplySpec(ISpecification<T> spec)
  {
    return SpecificationEvaluator<T>.GetQuery(context.Set<T>().AsQueryable(), spec);
  }
}