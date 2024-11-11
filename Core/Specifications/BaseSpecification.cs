using System.Linq.Expressions;

namespace Core;

public class BaseSpecification<T>(Expression<Func<T, bool>>? criteria) : ISpecification<T>
{
  protected BaseSpecification() : this(null) { }
  public Expression<Func<T, bool>>? Criteria => criteria;
  public Expression<Func<T, object>>? OrderBy { get; private set; }
  public Expression<Func<T, object>>? OrderByDesc { get; private set; }
  protected void AddOrderBy(Expression<Func<T, object>> orderByExpression)
  {
    OrderBy = orderByExpression;
  }
  protected void AddOrderByDesc(Expression<Func<T, object>> orderByDescExpression)
  {
    OrderByDesc = orderByDescExpression;
  }
}