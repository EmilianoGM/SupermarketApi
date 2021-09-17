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
    public class OrderDbContext : DataBaseContext<Order> , IOrderDbContext
    {
        private readonly IOrderProductsDbContext _opContext;
        private readonly IProductDbContext _productDbContext;
        public OrderDbContext(SupermarketDbContext context, IOrderProductsDbContext opContext, IProductDbContext productDbContext): base(context)
        {
            _opContext = opContext;
            _productDbContext = productDbContext;
        }

        public async Task AddWithProductAsync(Order entity, int[] productsIds)
        {
            await _items.AddAsync(entity);
            foreach (int id in productsIds)
            {
                var orderProduct = new OrderProducts();
                var product = await _productDbContext.FindByIdAsync(id);
                orderProduct.OrderId = entity.OrderId;
                orderProduct.Order = entity;
                orderProduct.Product = product;
                orderProduct.ProductId = id;
                await _opContext.AddAsync(orderProduct);
            }
            await _context.SaveChangesAsync();
        }

        public override async Task<IEnumerable<Order>> ListAsync()
        {
            return await _items.Include(o => o.Orderproducts).ThenInclude(op => op.Product).ThenInclude(p => p.Category).ToListAsync();
        }
    }
}
