using SupermarketApi.Abstractions.Contexts;
using SupermarketApi.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupermarketApi.DataAccess
{
    public class OrderProductsDbContext : DataBaseContext<OrderProducts>, IOrderProductsDbContext
    {
        public OrderProductsDbContext(SupermarketDbContext context) : base(context)
        {

        }


    }
}
