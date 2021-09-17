using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SupermarketApi.Abstractions.Contexts;
using SupermarketApi.Abstractions.Repositories;

namespace SupermarketApi.Repository
{
    /*
     * Repository classes basically encapsulate all logic to handle data access.
     * These repositories expose methods to list, create, edit and delete objects of a given model.
     */
    public class Repository<T> : IRepository<T>
    {
        protected readonly IDataBaseContext<T> _context;

        public Repository(IDataBaseContext<T> context)
        {
            _context = context;
        }

        public async Task AddAsync(T entity)
        {
            await _context.AddAsync(entity);
        }

        public async Task<T> FindByIdAsync(int id)
        {
            return await _context.FindByIdAsync(id);
        }

        public async Task<IEnumerable<T>> ListAsync()
        {
            return await _context.ListAsync();
        }

        public void Remove(T entity)
        {
             _context.Remove(entity);
        }

        public async Task<T> Update(T entity)
        {
            return await _context.Update(entity);
        }
    }
}
