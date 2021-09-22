using SupermarketApi.Abstractions.Applications;
using SupermarketApi.Abstractions.Communication;
using SupermarketApi.Abstractions.Repositories;
using SupermarketApi.Application.Communication;
using SupermarketApi.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupermarketApi.Application
{
    public class OrderApplication : Application<Order>, IOrderApplication
    {
        IOrderRepository _orderRepository;
        public OrderApplication(IOrderRepository repository): base(repository)
        {
            _orderRepository = repository;
        }

        public async Task<IResponseWrapper<Order>> AddWithProductsAsync(Order order, List<int> productIds)
        {
            try
            {
                await _orderRepository.AddWithProductAsync(order, productIds);                
                return new ResponseWrapper<Order>(order);
            }
            catch (Exception ex)
            {
                return new ResponseWrapper<Order>("An error ocurred while saving the entity.", ex.Message);
            }
        }

        public async Task<IResponseWrapper<Order>> UpdateWithProductsAsync(Order order, List<int> productsIds)
        {
            try
            {
                await _orderRepository.UpdateWithProductsAsync(order, productsIds);
                Order updatedOrder = await _orderRepository.FindByIdAsync(order.OrderId);
                return new ResponseWrapper<Order>(updatedOrder);
            }
            catch (Exception ex)
            {

                return new ResponseWrapper<Order>("An error ocurred while updating the entity.", ex.Message);
            }
        }
    }
}
