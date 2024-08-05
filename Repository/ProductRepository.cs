using BulkyWebb_New.Data;
using BulkyWebb_New.Models;
using BulkyWebb_New.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BulkyWebb_New.Repository
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        private  ApplicationDbContext _db;
        public ProductRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public Product Get(Func<Product, bool> predicate)
        {
             return _db.Products.FirstOrDefault(predicate);
        }

        public void Update(Product product)
        {
            _db.Products.Update(product);
        }

        // public IEnumerable<Category> GetAll()
        // {
        //     return _db.Categories.ToList();
        // }

        // public Category Get(Func<Category, bool> predicate)
        // {
        //     return _db.Categories.FirstOrDefault(predicate);
        // }

        // public void Add(Category category)
        // {
        //     _db.Categories.Add(category);
        // }

        // public void Update(Category category)
        // {
        //     _db.Categories.Update(category);
        // }

        // public void Remove(Category category)
        // {
        //     _db.Categories.Remove(category);
        // }

    }
}