using Core;

namespace Infrastructure;

public class SpecificationEvaluator<T> where T : EntityBase
{
  public static IQueryable<T> GetQuery(IQueryable<T> entity, ISpecification<T> spec)
  {
    if (spec.Criteria != null)
    {
      entity = entity.Where(spec.Criteria); // x => x.Brand == brand;
    }

    if (spec.OrderBy != null)
    {
      entity = entity.OrderBy(spec.OrderBy);
    }

    if (spec.OrderByDesc != null)
    {
      entity = entity.OrderByDescending(spec.OrderByDesc);
    }

    return entity;
  }

}