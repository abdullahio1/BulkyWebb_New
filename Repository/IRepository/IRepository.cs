using System.Linq.Expressions;

namespace BulkyWebb_New.Repository.IRepository
{
    public interface IRepository<T> where T : class
    {
        // T _ Category
        IEnumerable<T> GetAll();
        T Get(Expression<Func<T, bool>> filter);
        void Add(T entity);
        void Remove(T entity);
        void RemoveRange(IEnumerable<T> entity);
    }
}