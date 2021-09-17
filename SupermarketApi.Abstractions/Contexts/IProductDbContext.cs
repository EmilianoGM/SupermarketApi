using SupermarketApi.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupermarketApi.Abstractions.Contexts
{
    public interface IProductDbContext : IDataBaseContext<Product>
    {
        Task<IEnumerable<Product>> ListByNameAsync(string name);
        Task<IEnumerable<Product>> ListByCategoryAsync(int categoryId);
    }
}
