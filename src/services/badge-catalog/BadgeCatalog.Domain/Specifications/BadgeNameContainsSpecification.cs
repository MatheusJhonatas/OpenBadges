using System.Linq.Expressions;
using BadgeCatalog.Domain.Aggregates;

namespace BadgeCatalog.Domain.Specifications;

public sealed class BadgeNameContainsSpecification : Specification<BadgeClass>
{
    private readonly string _name;
    public BadgeNameContainsSpecification(string name)
    {
        _name = name;
    }
    public override Expression<Func<BadgeClass, bool>> ToExpression()
    {
        return badge => badge.Name.Contains(_name);
    }
}


