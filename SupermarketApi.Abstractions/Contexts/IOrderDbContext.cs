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
        Task AddWithProductAsync(Order entity, int[] productsIds);
    }
}
