using SupermarketApi.Abstractions.Communication;
using SupermarketApi.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupermarketApi.Abstractions.Applications
{
    public interface IOrderApplication : IApplication<Order>
    {
        Task<IResponseWrapper<Order>> AddAsyncWithProduct(Order order, List<int> productIds);
        Task<IResponseWrapper<Order>> UpdateWithProductsAsync(Order order, List<int> productsIds);
    }
}
