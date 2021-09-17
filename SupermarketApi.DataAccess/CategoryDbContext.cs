using SupermarketApi.Abstractions.Contexts;
using SupermarketApi.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupermarketApi.DataAccess
{
    public class CategoryDbContext : DataBaseContext<Category>, ICategoryDbContext
    {
        public CategoryDbContext(SupermarketDbContext context) : base(context)
        { }
    }
}
