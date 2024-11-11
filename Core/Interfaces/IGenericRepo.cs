namespace Core;

public interface IGenericRepo<T> where T : EntityBase
{
  Task<IReadOnlyList<T>> GetAllAsync();
  Task<T?> GetByIdAsync(int id);
  Task<T?> GetEntityWithSpec(ISpecification<T> spec);
  Task<IReadOnlyList<T>> GetAllWithSpec(ISpecification<T> spec);
  void Update(T entity);
  void Add(T entity);
  void Delete(T entity);
  bool Exists(int id);
  Task<bool> SaveChangesAsync();
}