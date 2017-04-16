using System;
using System.Linq.Expressions;

namespace WoaW.Patterns.Specification
{
    public abstract class LinqSpecification<T> : CompositeSpecification<T>
    {
        public abstract Expression<Func<T, bool>> AsExpression();

        public override bool IsSatisfiedBy(T entity)
        {
            Func<T, bool> predicate = AsExpression().Compile();
            return predicate(entity);
        }
    }
}
