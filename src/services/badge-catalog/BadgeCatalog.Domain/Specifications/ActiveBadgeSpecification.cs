using System.Linq.Expressions;
using BadgeCatalog.Domain.Aggregates;
namespace BadgeCatalog.Domain.Specifications;

public sealed class ActiveBadgeSpecification : Specification<BadgeClass>
{
    public override Expression<Func<BadgeClass, bool>> ToExpression()
    {
        return badge => badge.IsActive;
    }
}