using SupermarketApi.Abstractions.Communication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupermarketApi.Abstractions.Applications
{
    public interface IApplication<T>
    {
        Task<IResponseWrapper<IEnumerable<T>>> ListAsync();
        Task<IResponseWrapper<T>> AddAsync(T entity);
        Task<IResponseWrapper<T>> FindByIdAsync(int id);
        Task<IResponseWrapper<T>> Remove(int id);
        Task<IResponseWrapper<T>> Update(T entity);
    }

}
