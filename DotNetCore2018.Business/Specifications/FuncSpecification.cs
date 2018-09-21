using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DotNetCore2018.Business.Specifications
{
    public class FuncSpecification<T> : Specification<T> where T: class
    {
        private readonly Expression<Func<T, bool>> _expression;

        public FuncSpecification(Expression<Func<T, bool>> expression)
        {
            _expression = expression;
        }

        public override Expression<Func<T, bool>> Expr => _expression;
    }
}
