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
    public class OrderProductsDbContext : DataBaseContext<OrderProducts>, IOrderProductsDbContext
    {
        public OrderProductsDbContext(SupermarketDbContext context) : base(context)
        {

        }

        public async Task<IEnumerable<OrderProducts>> ListByOrderIdAsync(int id)
        {
            return await _items.Where(op => op.OrderId == id).ToListAsync();
        }

        public async Task<IEnumerable<int>> ListProductsIdsByOrderId(int id)
        {
            List<int> producstIds = new List<int>();
            List<OrderProducts> orderProducts = (List<OrderProducts>)await ListByOrderIdAsync(id);
            foreach (OrderProducts orderProduct in orderProducts)
            {
                producstIds.Add(orderProduct.ProductId);
            }
            return producstIds;
        }
    }
}
