using BulkyWebb_New.Models;
using System;
using System.Collections.Generic;

namespace BulkyWebb_New.Repository.IRepository
{
    public interface IProductRepository : IRepository<Product>
    {
        Product Get(Func<Product, bool> predicate);  
        void Add(Product product);
        void Update(Product product);
        void Remove(Product product);
    }
}