using BadgeCatalog.Domain.Aggregates;

namespace BadgeCatalog.Domain.Specifications;

public class InactiveBadgeSpecification : Specification<BadgeClass>
{
    public override System.Linq.Expressions.Expression<Func<BadgeClass, bool>> ToExpression()
    {
        return badge => !badge.IsActive;
    }
}