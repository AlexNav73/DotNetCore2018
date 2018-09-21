using System;
using System.Linq;
using System.Linq.Expressions;

namespace DotNetCore2018.Business.Specifications
{
    public class AndSpecification<T> : Specification<T> where T : class
    {
        private readonly Specification<T> _left;
        private readonly Specification<T> _right;

        public AndSpecification(Specification<T> left, Specification<T> right)
        {
            _left = left;
            _right = right;
        }

        public override Expression<Func<T, bool>> Expr
        {
            get
            {
                var leftExpr = _left.Expr;
                var rightExpr = _right.Expr;

                if (leftExpr == null) { return rightExpr; }
                if (rightExpr == null) { return leftExpr; }

                var parameter = Expression.Parameter(typeof(T), "x");

                var left = new ReplaceExpressionVisitor(leftExpr.Parameters.Single(), parameter)
                    .Visit(leftExpr.Body);

                var right = new ReplaceExpressionVisitor(rightExpr.Parameters.Single(), parameter)
                    .Visit(rightExpr.Body);

                return Expression.Lambda<Func<T, bool>>(Expression.AndAlso(left, right), parameter);
            }
        }

        class ReplaceExpressionVisitor : ExpressionVisitor
        {
            private readonly Expression _oldValue;
            private readonly Expression _newValue;

            public ReplaceExpressionVisitor(Expression oldValue, Expression newValue)
            {
                _oldValue = oldValue;
                _newValue = newValue;
            }

            public override Expression Visit(Expression node)
            {
                if (node == _oldValue)
                    return _newValue;
                return base.Visit(node);
            }
        }
    }
}
