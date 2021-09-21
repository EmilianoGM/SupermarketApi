using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SupermarketApi.WebApi.Resources
{
    public class OrderResource
    {
        public int OrderId { get; set; }
        public DateTime? Date { get; set; }
        public float? TotalPrice { get; set; }
        public string Address { get; set; }

        public List<OrderProductsResource> Orderproducts { get; set; }
    }
}
