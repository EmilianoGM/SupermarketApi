using SupermarketApi.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SupermarketApi.Abstractions.Repositories
{
    public interface IProductRepository : IRepository<Product>
    {
        Task<IEnumerable<Product>> ListByNameAsync(string name);
        Task<IEnumerable<Product>> ListByCategoryAsync(int categoryId);
    }
}
