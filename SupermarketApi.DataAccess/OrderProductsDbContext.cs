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

        public override async Task AddAsync(OrderProducts entity)
        {
            await _items.AddAsync(entity);
        }

        public override void Remove(OrderProducts entity)
        {
            _items.Remove(entity);
        }

        public async Task<IEnumerable<OrderProducts>> ListByOrderIdAsync(int id)
        {
            return await _items.Where(op => op.OrderId == id).ToListAsync();
        }

        public IEnumerable<int> ListProductsIdsByOrderId(IEnumerable<OrderProducts> orderProductsEnumerable)
        {
            List<int> producstIds = new List<int>();
            foreach (OrderProducts orderProduct in orderProductsEnumerable)
            {
                producstIds.Add(orderProduct.ProductId);
            }
            return producstIds;
        }
    }
}
