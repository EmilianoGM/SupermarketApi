using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SupermarketAPI.Persistance.Contexts;

namespace SupermarketAPI.Persistance.Repositories
{
    /*
     * Repository classes basically encapsulate all logic to handle data access.
     * These repositories expose methods to list, create, edit and delete objects of a given model.
     */
    public abstract class BaseRepository
    {
        protected readonly SupermarketDbContext _context;

        public BaseRepository(SupermarketDbContext context)
        {
            _context = context;
        }
    }
}
