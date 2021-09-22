using SupermarketApi.Abstractions.Contexts;
using SupermarketApi.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupermarketApi.DataAccess
{
    public class ProductDbContext : DataBaseContext<Product>, IProductDbContext
    {
        public ProductDbContext(SupermarketDbContext context): base(context)
        {
       
        }

        public async override Task<IEnumerable<Product>> ListAsync()
        {
            return await _items.Include(p => p.Category).ToListAsync(); 
        }

        public async Task<IEnumerable<Product>> ListByCategoryAsync(int categoryId)
        {
            return await _items.Where(p => p.CategoryId == categoryId).Include(p => p.Category).ToListAsync();
        }

        public async Task<IEnumerable<Product>> ListByNameAsync(string name)
        {
            return await _items.Where(p => p.Name.Equals(name,StringComparison.OrdinalIgnoreCase)).Include(p => p.Category).ToListAsync();
        }
    }
}
