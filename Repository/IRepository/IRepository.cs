using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BulkyWebb_New.Repository.IRepository
{
    internal interface IRepository<T> where T : class
    {
        // T _ Category
        IEnumerable<T> GetAll(string? IncludeProperties = null);
        T Get(Expression<Func<T, bool>> filter, string? IncludeProperties = null);
        void Add(T entity);
        void Remove(T entity);
        void RemoveRange(IEnumerable<T> entity);
    }
}