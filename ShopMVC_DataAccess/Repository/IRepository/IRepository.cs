using System.Linq.Expressions;

namespace ShopMVC_DataAccess
{
	public interface IRepository<T> where T : class
	{
		public T? FindById(int id);
		public IEnumerable<T> GetAll(Expression<Func<T, bool>> predicate = null,
			Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            List<string> includeProperties = null,
            bool isTracking = true);

		public T? FirstOrDefault(Expression<Func<T, bool>> predicate = null,
            List<string> includeProperties = null,
            bool isTracking = true);

		public void Add(T entity);
		public void Remove(T entity);
		public void Save();
	}
}
