using SupermarketApi.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SupermarketApi.Abstractions.Applications;
using SupermarketApi.Abstractions.Repositories;

namespace SupermarketApi.Application
{
    public class CategoryApplication : Application<Category>, ICategoryApplication
    {
        public CategoryApplication(ICategoryRepository repository): base(repository)
        {
            
        }
    }
}
