using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using DotNetCore2018.Data;

namespace DotNetCore2018.Business.Specifications
{
    public class IdSpecification<T> : Specification<T> where T : class, IHasId<int>
    {
        private readonly int? _id = null;
        private readonly IList<int> _ids = null;

        private IdSpecification() { }

        public IdSpecification(int? id)
        {
            _id = id;
        }

        public IdSpecification(params int[] ids)
        {
            _ids = ids;
        }

        public override Expression<Func<T, bool>> Expr
        {
            get
            {
                if (_id != null) { return x => x.Id == _id.Value; }
                if (_ids != null && _ids.Count > 0) { return x => _ids.Contains(x.Id); }
                return x => true;
            }
        }
    }
}
