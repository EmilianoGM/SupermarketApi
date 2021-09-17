using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SupermarketApi.Entities
{
    public partial class Product
    {
        public Product()
        {
            Orderproducts = new HashSet<OrderProducts>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public short? QuantityInPackage { get; set; }
        public EUnitOfMeasurement UnitOfMeasurement { get; set; }
        public int? CategoryId { get; set; }
        public float? Price { get; set; }
        public string ImgUrl { get; set; }

        public virtual Category Category { get; set; }
        public virtual ICollection<OrderProducts> Orderproducts { get; set; }
    }
    /*
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public short QuantityInPackage { get; set; }
        public EUnitOfMeasurement UnitOfMeasurement { get; set; }
        public string ImgUrl { get; set; }
        public float Price { get; set; }

        public int CategoryId { get; set; }
        public Category Category { get; set; }

        public IList<OrderProducts> OrderProducts { get; set; } = new List<OrderProducts>();

    }*/
}
