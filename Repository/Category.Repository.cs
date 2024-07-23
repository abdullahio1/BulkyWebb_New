using BulkyWebb_New.Data;
using BulkyWebb_New.Models;
using BulkyWebb_New.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BulkyWebb_New.Repository
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ApplicationDbContext _db;

        public CategoryRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public IEnumerable<Category> GetAll()
        {
            return _db.Categories.ToList();
        }

        public Category Get(Func<Category, bool> predicate)
        {
#pragma warning disable CS8603 // Possible null reference return.
            return _db.Categories.FirstOrDefault(predicate);
#pragma warning restore CS8603 // Possible null reference return.
        }

        public void Add(Category category)
        {
            _db.Categories.Add(category);
            Save();
        }

        public void Update(Category category)
        {
            _db.Categories.Update(category);
            Save();
        }

        public void Remove(Category category)
        {
            _db.Categories.Remove(category);
            Save();
        }

        public void Save()
        {
            _db.SaveChanges();
        }
    }
}



//2.------------------------------------------------
//  namespace BulkyWebb_New.Repository
// {
//      public class CategoryRepository : ICategoryRepository
//     {
//         private readonly ApplicationDbContext _db;

//         public CategoryRepository(ApplicationDbContext db)
//         {
//             _db = db;
//         }

//         public IEnumerable<Category> GetAll()
//         {
//             return _db.Categories.ToList();
//         }

//         public Category Get(int id)
//         {
//             return _db.Categories.FirstOrDefault(c => c.Id == id);
//         }

//         public void Add(Category category)
//         {
//             _db.Categories.Add(category);
//             Save();
//         }

//         public void Update(Category category)
//         {
//             _db.Categories.Update(category);
//             Save();
//         }

//         public void Delete(int id)
//         {
//             var category = _db.Categories.FirstOrDefault(c => c.Id == id);
//             if (category != null)
//             {
//                 _db.Categories.Remove(category);
//                 Save();
//             }
//         }

//         public void Save()
//         {
//             _db.SaveChanges();
//         }
//     }
    //1.------------------------------------------------
    // public class CategoryRepository : Repository<Category>, ICategoryRepository
    // {
    //     private ApplicationDbContext _db;
    //     public CategoryRepository(ApplicationDbContext db) : base(db)
    //     {
    //         _db = db;
    //     }
    //     public void Save()
    //     {
    //         _db.SaveChanges();
    //     }
    //     public void Update(Category obj)
    //     {
    //         _db.Categories.Update(obj);
    //     }
    // }
