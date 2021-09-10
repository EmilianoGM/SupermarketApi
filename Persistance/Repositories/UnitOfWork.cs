using SupermarketAPI.Domain.Repositories;
using SupermarketAPI.Persistance.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SupermarketAPI.Persistance.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly SupermarketDbContext _context;

        public UnitOfWork(SupermarketDbContext context)
        {
            _context = context;
        }

        public async Task CompleteAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
