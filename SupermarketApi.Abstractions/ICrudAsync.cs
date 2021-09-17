using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupermarketApi.Abstractions
{
    public interface ICrudAsync<T>
    {
        Task<IEnumerable<T>> ListAsync();
        Task AddAsync(T entity);
        Task<T> FindByIdAsync(int id);
        Task<T> Update(T entity);
        void Remove(T entity);
    }
}
