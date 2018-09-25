using DotNetCore2018.Business.Specifications;
using DotNetCore2018.Data;
using DotNetCore2018.Data.Entities;

namespace DotNetCore2018.Business.Services.Interfaces
{
    public interface IDataService
    {
        T[] GetAll<T>() where T : class, IHasId<int>;
        T[] GetAllBy<T>(Specification<T> specification) where T : class, IHasId<int>;
        T GetBy<T>(Specification<T> specification) where T : class, IHasId<int>;
        void Update<T>(T category) where T : class, IHasId<int>;
        void Add<T>(T category) where T : class, IHasId<int>;
    }
}