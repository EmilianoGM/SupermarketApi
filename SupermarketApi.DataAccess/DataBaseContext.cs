using Microsoft.EntityFrameworkCore;
using SupermarketApi.Abstractions.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupermarketApi.DataAccess
{
    public class DataBaseContext<T> : IDataBaseContext<T> where T : class
    {
        protected readonly DbSet<T> _items;
        protected readonly SupermarketDbContext _context;

        public DataBaseContext(SupermarketDbContext context)
        {
            _context = context;
            _items = context.Set<T>();
        }

        public virtual async Task<IEnumerable<T>> ListAsync()
        {
            return await _items.ToListAsync();
        }

        public virtual async Task AddAsync(T entity)
        {
            await _items.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public virtual async Task<T> FindByIdAsync(int id)
        {
            return await _items.FindAsync(id);
        }

        public virtual async Task<T> Update(T entity)
        {
            _items.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public virtual async void Remove(T entity)
        {
            _items.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<int> SaveChangesAsync()
        {
           return await _context.SaveChangesAsync();
        }
    }
}
