using Microsoft.EntityFrameworkCore;
using SupermarketApi.Abstractions.Contexts;
using SupermarketApi.Abstractions.Repositories;
using SupermarketApi.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SupermarketApi.Repository
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        IProductDbContext _productContext;
        public ProductRepository(IProductDbContext context): base(context)
        {
            _productContext = context;
        }
        public async Task<IEnumerable<Product>> ListByCategoryAsync(int categoryId)
        {
            return await _productContext.ListByCategoryAsync(categoryId);
        }

        public async Task<IEnumerable<Product>> ListByNameAsync(string name)
        {
            return await _productContext.ListByNameAsync(name);
        }
    }
}
