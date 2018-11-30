using System.Linq;
using DotNetCore2018.Business.Services.Interfaces;
using DotNetCore2018.Business.Specifications;
using DotNetCore2018.Core;
using DotNetCore2018.Data;
using Microsoft.EntityFrameworkCore;

namespace DotNetCore2018.Business.Services
{
    public sealed class DataService : IDataService
    {
        private readonly AppContext _context;

        public DataService(AppContext context)
        {
            _context = context;
        }

        public void Add<T>(T obj) where T : class, IHasId<int>
        {
            _context.Set<T>().Add(obj);
            _context.SaveChanges();
        }

        public IQueryable<T> GetAll<T>() where T : class, IHasId<int>
        {
            return _context.Set<T>();
        }

        public IQueryable<T> GetAllBy<T>(Specification<T> specification)
            where T : class, IHasId<int>
        {
            return _context.Set<T>().Where(specification.Expr);
        }

        public T GetBy<T>(Specification<T> specification) where T : class, IHasId<int>
            => _context.Set<T>().FirstOrDefault(specification.Expr);

        public void Update<T>(T obj) where T : class, IHasId<int>
        {
            _context.Attach(obj).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public void Delete<T>(T obj) where T : class, IHasId<int>
        {
            _context.Set<T>().Remove(obj);
            _context.SaveChanges();
        }
    }
}