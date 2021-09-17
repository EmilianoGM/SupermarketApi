using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SupermarketApi.Abstractions.Contexts;
using SupermarketApi.Abstractions.Repositories;
using SupermarketApi.Entities;

namespace SupermarketApi.Repository
{

    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        public CategoryRepository(ICategoryDbContext context): base(context)
        {
            
        }
        
    }
}
