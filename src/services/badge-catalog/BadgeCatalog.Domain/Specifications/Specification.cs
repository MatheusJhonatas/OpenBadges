using System.Linq.Expressions;

namespace BadgeCatalog.Domain.Specifications;

public abstract class Specification<T>
{
    public abstract Expression<Func<T, bool>> ToExpression();

    public Func<T, bool> ToFunc()
    {
        return ToExpression().Compile();
    }
}