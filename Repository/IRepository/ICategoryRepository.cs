using BulkyWebb_New.Models;
using System;
using System.Collections.Generic;

namespace BulkyWebb_New.Repository.IRepository
{
    public interface ICategoryRepository
    {
        IEnumerable<Category> GetAll();
        Category Get(Func<Category, bool> predicate);  
        void Add(Category category);
        void Update(Category category);
        void Remove(Category category);  
    }
}


// namespace BulkyWebb_New.Repository.IRepository
// {
//     namespace BulkyWebb_New.Repository.IRepository
// {
//     public interface ICategoryRepository
//     {
//         IEnumerable<Category> GetAll();
//         Category Get(Func<Category, bool> predicate);

//         Category Get(int id);
//         void Add(Category category);
//         void Update(Category category);
//         void Remove(Category category);
//         void Save();
//     }
// }
    // internal interface ICategoryRepository : IRepository<Category>
    // {
    //     void Update(Category obj);
    //     void Save();
    // }
