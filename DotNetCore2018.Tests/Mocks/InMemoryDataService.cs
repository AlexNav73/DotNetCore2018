using System.Collections.Generic;
using System.Linq;
using DotNetCore2018.Business.Services.Interfaces;
using DotNetCore2018.Business.Specifications;
using DotNetCore2018.Core;

namespace DotNetCore2018.Tests.Mocks
{
    internal class InMemoryDataService : IDataService
    {
        private readonly List<IHasId<int>> _data = new List<IHasId<int>>();
        private int _idxPtr = 0;

        public void Add<T>(T obj) where T : class, IHasId<int>
        {
            _data.Add(obj);
            obj.Id = _idxPtr++;
        }

        public void Delete<T>(T obj) where T : class, IHasId<int>
            => _data.Remove(obj);

        public IQueryable<T> GetAll<T>() where T : class, IHasId<int>
            => _data.Cast<T>().AsQueryable();

        public IQueryable<T> GetAllBy<T>(Specification<T> specification) where T : class, IHasId<int>
            => _data.Cast<T>().Where(specification.Expr.Compile()).AsQueryable();

        public T GetBy<T>(Specification<T> specification) where T : class, IHasId<int>
            => _data.Cast<T>().FirstOrDefault(specification.Expr.Compile());

        public void Update<T>(T obj) where T : class, IHasId<int>
        {
            var old = _data.Find(x => x.Id == obj.Id);
            _data.Remove(old);
            Add(obj);
        }
    }
}