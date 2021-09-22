using SupermarketApi.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupermarketApi.Abstractions.Contexts
{
    public interface IOrderProductsDbContext : IDataBaseContext<OrderProducts>
    {
        Task<IEnumerable<OrderProducts>> ListByOrderIdAsync(int id);
        IEnumerable<int> ListProductsIdsByOrderId(IEnumerable<OrderProducts> orderProductsEnumerable);
    }
}
