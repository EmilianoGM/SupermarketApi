using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SupermarketApi.WebApi.Resources
{
    public class SaveOrderResource
    {
        [Required]
        public string Date { get; set; }
        [Required]
        public float TotalPrice { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public List<int> ProductsId { get; set; }
    }
}
