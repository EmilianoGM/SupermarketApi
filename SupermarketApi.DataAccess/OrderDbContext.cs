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

        /// <summary>
        /// Devuelve un listado de ordenes incluyendo los productos relacionados y sus respectivas categorias.
        /// </summary>
        /// <returns>IEnumerable con la data relacionada</returns>
        public override async Task<IEnumerable<Order>> ListAsync()
        {
            return await _items.Include(o => o.Orderproducts).ThenInclude(op => op.Product).ThenInclude(p => p.Category).ToListAsync();
        }

        /// <summary>
        /// Agrega una nueva Order junto con un listado de ids de los productos relacionados, a la base de datos.
        /// Genera una nueva entidad del tipo OrderProducts por cada nuevo id de producto en la lista.
        /// </summary>
        /// <param name="entity">Order a agregar</param>
        /// <param name="productsIds">List de ids de productos</param>
        /// <returns></returns>
        public async Task AddWithProductsAsync(Order entity, List<int> productsIds)
        {
            // *****REVISAR*****
            using var transaction = _context.Database.BeginTransaction();
            await _items.AddAsync(entity);
            await SaveChangesAsync();
            int orderId = entity.OrderId;
            foreach (int id in productsIds)
            {
                OrderProducts orderProducts = new OrderProducts();
                orderProducts.OrderId = entity.OrderId;
                orderProducts.ProductId = id;
                await _opContext.AddAsync(orderProducts);
            }
            await _opContext.SaveChangesAsync();
            transaction.Commit();
            await transaction.DisposeAsync();
        }

       
        public override async Task<Order> FindByIdAsync(int id)
        {
            return await _items.Include(o => o.Orderproducts).ThenInclude(op => op.Product).ThenInclude(p => p.Category).FirstOrDefaultAsync(o => o.OrderId == id);
        }


        /// <summary>
        /// Actualiza una Order junto con la lista de productos relacionados.
        /// Obtiene la lista de OrderProducts relacionados a la Order y la compara con la lista de ids provista.
        /// A partir de la misma borra las intancias de OrderProducts cuyo ProductId no se encuentre en productsIds
        /// y genera una nueva entidad OrderProducts por cada id de productos que sólo se encuentre en productsIds.
        /// </summary>
        /// <param name="entity">Order a actualizar</param>
        /// <param name="productsIds">Lista de ids de productos</param>
        /// <returns></returns>
        public async Task UpdateWithProductsAsync(Order entity, List<int> productsIds)
        {
            // *****REVISAR*****
            using var transaction = _context.Database.BeginTransaction();
            List<OrderProducts> orderProductsList = (List<OrderProducts>)await _opContext.ListByOrderIdAsync(entity.OrderId);
            List<int> orderProductsIds = (List<int>) _opContext.ListProductsIdsByOrderId(orderProductsList);
            List<int> productsIdsToRemove = orderProductsIds.Except(productsIds).ToList();
            List<int> productsIdsToAdd = productsIds.Except(orderProductsIds).ToList();
            if(productsIdsToRemove.Count > 0)
            {
                foreach (int id in productsIdsToRemove)
                {
                    OrderProducts orderProducts = orderProductsList.FirstOrDefault(op => op.ProductId == id);
                    _opContext.Remove(orderProducts);
                }
            }
            if(productsIdsToAdd.Count > 0)
            {
                foreach (int id in productsIdsToAdd)
                {
                    OrderProducts orderProducts = new OrderProducts();
                    orderProducts.OrderId = entity.OrderId;
                    orderProducts.ProductId = id;
                    await _opContext.AddAsync(orderProducts);
                }
            }
            _items.Update(entity);
            await SaveChangesAsync();
            transaction.Commit();
            await transaction.DisposeAsync();
        }
    }
}
