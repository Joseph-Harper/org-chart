using OrgChart.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace OrgChart.Core.Specifications
{
    public abstract class Specification<T> : ISpecification<T>
    {
        public Specification(Expression<Func<T, bool>> predicate)
        {
            Predicate = predicate;
        }

        public Expression<Func<T, bool>> Predicate { get; }
        public List<Expression<Func<T, object>>> Includes { get; } = new List<Expression<Func<T, object>>>();

        protected virtual void AddInclude(Expression<Func<T, object>> includeExpression)
        {
            Includes.Add(includeExpression);
        }
    }
}
