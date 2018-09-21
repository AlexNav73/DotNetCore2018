using System;
using System.Linq.Expressions;

namespace DotNetCore2018.Business.Specifications
{
    public abstract class Specification<T> where T : class
    {
        public abstract Expression<Func<T, bool>> Expr { get; }

        public Specification<T> And(Specification<T> left, Specification<T> right)
            => new AndSpecification<T>(left, right);

        public static Specification<T> operator&(Specification<T> left, Specification<T> right)
            => new AndSpecification<T>(left, right);
    }
}
