using SupermarketApi.Abstractions.Contexts;
using SupermarketApi.Abstractions.Repositories;
using SupermarketApi.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupermarketApi.Repository
{
    public class OrderRepository : Repository<Order>, IOrderRepository
    {
        private readonly IOrderDbContext _orderContext;
        public OrderRepository(IOrderDbContext context) : base(context)
        {
            _orderContext = context;
        }

        public async Task AddWithProductAsync(Order entity, List<int> productsIds)
        {
            await _orderContext.AddWithProductsAsync(entity, productsIds);
        }

        public async Task UpdateWithProductsAsync(Order entity, List<int> productsIds)
        {
            await _orderContext.UpdateWithProductsAsync(entity, productsIds);
        }
    }
}
