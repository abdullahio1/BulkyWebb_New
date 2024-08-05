using BulkyWebb_New.Data;
using BulkyWebb_New.Models;
using BulkyWebb_New.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace BulkyWebb_New.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _db;
        public ICategoryRepository Category { get;  private set; }
        public IProductRepository Product { get; private set; } 
        public UnitOfWork(ApplicationDbContext db)
        {
            _db = db;
            Category = new CategoryRepository(_db); // Initialize the CategoryRepository
            Product = new ProductRepository(_db); // Initialize the ProductRepository
        }
        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
// namespace BulkyWebb_New.Repository
// {
//     public class UnitOfWork : IUnitOfWork
//     {
//          private ApplicationDbContext _db;
//          public UnitOfWork(ApplicationDbContext db)
//         {
//             _db = db;
//             category = new CategoryRepository(_db);
//         }
//         public ICategoryRepository categoryRepository { get;private set; }


//         public void Save()
//         {
//             _db.SaveChanges();
//         }
//     }
// }