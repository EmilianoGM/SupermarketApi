using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SupermarketApi.WebApi.Resources
{
    public class OrderProductsResource
    {
        public virtual ProductResource Product
        {
            get; set;
        }
    }
}
