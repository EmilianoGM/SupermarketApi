using SupermarketApi.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupermarketApi.Abstractions.Contexts
{
    public interface IOrderDbContext : IDataBaseContext<Order>
    {
        Task AddWithProductsAsync(Order entity, List<int> productsIds);
        Task UpdateWithProductsAsync(Order entity, List<int> productsIds);
    }
}
