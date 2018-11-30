using System.Linq;
using DotNetCore2018.Business.Specifications;
using DotNetCore2018.Core;

namespace DotNetCore2018.Business.Services.Interfaces
{
    public interface IDataService
    {
        IQueryable<T> GetAll<T>() where T : class, IHasId<int>;
        IQueryable<T> GetAllBy<T>(Specification<T> specification) where T : class, IHasId<int>;
        T GetBy<T>(Specification<T> specification) where T : class, IHasId<int>;
        void Update<T>(T obj) where T : class, IHasId<int>;
        void Add<T>(T obj) where T : class, IHasId<int>;
        void Delete<T>(T obj) where T : class, IHasId<int>;
    }
}