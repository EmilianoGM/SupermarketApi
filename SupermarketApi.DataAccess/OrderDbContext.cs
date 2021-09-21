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
        public OrderDbContext(SupermarketDbContext context, IOrderProductsDbContext opContext): base(context)
        {
            _opContext = opContext;
        }

        public async Task AddWithProductsAsync(Order entity, List<int> productsIds)
        {
            // REVISAR
            // await _items.AddAsync(entity);
            using var transaction = _context.Database.BeginTransaction();
            _items.Add(entity);
            await _context.SaveChangesAsync();
            int orderId = entity.OrderId;
            //Console.WriteLine($"Order id:{orderId}");
            foreach (int id in productsIds)
            {
                var orderProduct = new OrderProducts();
                orderProduct.OrderId = entity.OrderId;
                orderProduct.ProductId = id;
                await _opContext.AddAsync(orderProduct);
            }
            _opContext.SaveChangesAsync();
            transaction.Commit();
            await transaction.DisposeAsync();
        }

        public override async Task<IEnumerable<Order>> ListAsync()
        {
            return await _items.Include(o => o.Orderproducts).ThenInclude(op => op.Product).ThenInclude(p => p.Category).ToListAsync();
        }

        public override async Task<Order> FindByIdAsync(int id)
        {
            return await _items.Include(o => o.Orderproducts).ThenInclude(op => op.Product).ThenInclude(p => p.Category).FirstOrDefaultAsync(o => o.OrderId == id);
        }

        public async Task UpdateWithProductsAsync(Order entity, List<int> productsIds)
        {
            using var transaction = _context.Database.BeginTransaction();
            List<OrderProducts> orderProductsList = (List<OrderProducts>)await _opContext.ListByOrderIdAsync(entity.OrderId);
            List<int> orderProductsIds = (List<int>)await _opContext.ListProductsIdsByOrderId(entity.OrderId);
            List<int> oldProductsIds = orderProductsIds.Except(productsIds).ToList();
            List<int> newProductsIds = productsIds.Except(orderProductsIds).ToList();
            if(oldProductsIds.Count > 0)
            {
                foreach (int id in oldProductsIds)
                {
                    OrderProducts orderProducts = orderProductsList.FirstOrDefault(op => op.ProductId == id);
                    _opContext.Remove(orderProducts);
                }
            }
            if(newProductsIds.Count > 0)
            {
                foreach (int id in newProductsIds)
                {
                    OrderProducts orderProducts = new OrderProducts();
                    orderProducts.OrderId = entity.OrderId;
                    orderProducts.ProductId = id;
                    await _opContext.AddAsync(orderProducts);
                }
            }
            _items.Update(entity);
            base.SaveChangesAsync();
            transaction.Commit();
            await transaction.DisposeAsync();
        }
    }
}
