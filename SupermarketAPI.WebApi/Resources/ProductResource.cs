using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SupermarketApi.WebApi.Resources
{
    public class ProductResource
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public short? QuantityInPackage { get; set; }
        public string UnitOfMeasurement { get; set; }
        //public int? CategoryId { get; set; }
        public float? Price { get; set; }
        public string ImgUrl { get; set; }

        /*
        public int Id { get; set; }
        public string Name { get; set; }
        public int QuantityInPackage { get; set; }
        public string UnitOfMeasurement { get; set; }
        public float Price { get; set; }
        public string ImgUrl { get; set; }*/
        public CategoryNameResource Category { get; set; }

    }
}
