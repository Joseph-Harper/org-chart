using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace OrgChart.Core.Interfaces
{
    public interface ISpecification<T>
    {
        Expression<Func<T, bool>> Predicate { get; }
        List<Expression<Func<T, object>>> Includes { get; }
    }
}
