using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SupermarketApi.WebApi.Resources
{
    public class SaveOrderResource
    {
        public string Date { get; set; }
        public float TotalPrice { get; set; }
        public string Address { get; set; }
        public int[] ProductsId { get; set; }
    }
}
