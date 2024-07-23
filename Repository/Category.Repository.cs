using BulkyWebb_New.Models;
using BulkyWebb_New.Repository.IRepository;
using System.Linq.Expressions;

namespace BulkyWebb_New.Repository;

public class CategoryRepository : ICategoryRepository
{
    public void Add(Category entity)
    {
        throw new NotImplementedException();
    }

    public Category Get(Expression<Func<Category, bool>> filter)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<Category> GetAll()
    {
        throw new NotImplementedException();
    }

    public void Remove(Category entity)
    {
        throw new NotImplementedException();
    }

    public void RemoveRange(IEnumerable<Category> entity)
    {
        throw new NotImplementedException();
    }

    public void Save()
    {
        throw new NotImplementedException();
    }

    public void Update(Category obj)
    {
        throw new NotImplementedException();
    }
}
