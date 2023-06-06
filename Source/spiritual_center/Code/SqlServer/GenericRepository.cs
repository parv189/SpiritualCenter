using Microsoft.EntityFrameworkCore;
using spiritual_center.Models;

namespace spiritual_center.Code.SqlServer
{
    public class GenericRepository<T> : IRepository<T> where T : class
    {
        protected readonly UserContext _context;
        public GenericRepository(UserContext context)
        {
            _context = context;
        }

        public T Add(T entity)
        {
            _context.Set<T>().Add(entity);
            _context.SaveChanges();
            return entity;
        }

        public T Delete(T entity)
        {
            _context.Set<T>().Remove(entity);
            _context.SaveChanges();
            return entity;
        }

        public IEnumerable<T> Find(Func<T, bool> predicate)
        {
            return _context.Set<T>().Where(predicate);
        }

        public IEnumerable<T> Index()
        {
            return _context.Set<T>();
        }

        public bool IsExist(Func<T, bool> predicate)
        {
            return _context.Set<T>().Any(predicate);
        }

        public T Update(T entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            _context.SaveChanges();
            return entity;
        }
    }
}
