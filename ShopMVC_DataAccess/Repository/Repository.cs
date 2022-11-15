using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;


namespace ShopMVC_DataAccess
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly ApplicationDbContext db;
        protected DbSet<T> dbSet;

        public Repository(ApplicationDbContext database)
        {
            db = database;
            dbSet = db.Set<T>();   
        }

        public void Add(T entity)
        { 
            dbSet.Add(entity);
        }

        public T? FindById(int id)
        {
            return dbSet.Find(id);
        }

        public T? FirstOrDefault(Expression<Func<T, bool>> predicate = null, List<string> includeProperties = null, bool isTracking = true)
        {
            IQueryable<T> query = dbSet;

            if (predicate != null)
            {
                query = query.Where(predicate);
            }

            if (includeProperties != null)
            {
                foreach (var property in includeProperties)
                {
                    query = query.Include(property);
                }
            }

            if (isTracking == false)
            {
                query = query.AsNoTracking();
            }

            return query.FirstOrDefault();
        }

        public IEnumerable<T> GetAll(Expression<Func<T, bool>> predicate = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, List<string> includeProperties = null, bool isTracking = true)
        {
            IQueryable<T> query = dbSet;

            if (predicate != null)
            {
                query = query.Where(predicate);
            }

            if (includeProperties != null)
            {
                foreach (var property in includeProperties)
                {
                    query = query.Include(property);
                }
            }

            if (orderBy != null)
            {
                query = orderBy(query);
            }

            if (isTracking == false)
            {
                query = query.AsNoTracking();
            }

            return query.ToList();
        }

        public void Remove(T entity)
        {
            dbSet.Remove(entity);
        }

        public void Save()
        {
            db.SaveChanges();
        }
    }
}
