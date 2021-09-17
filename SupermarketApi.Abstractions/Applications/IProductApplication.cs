using SupermarketApi.Abstractions.Communication;
using SupermarketApi.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupermarketApi.Abstractions.Applications
{
    public interface IProductApplication : IApplication<Product>
    {
        Task<IResponseWrapper<IEnumerable<Product>>> ListByNameAsync(string name);
        Task<IResponseWrapper<IEnumerable<Product>>> ListByCategoryAsync(int categoryId);
    }
}
