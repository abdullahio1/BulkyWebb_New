using BulkyWebb_New.Models;

namespace BulkyWebb_New.Repository.IRepository
{
    public interface ICategoryRepository : IRepository<Category>
    {
        void Update(Category obj);
        void Save();
    }
}