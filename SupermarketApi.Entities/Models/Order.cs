using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupermarketApi.Entities
{
    public partial class Order
    {
        public Order()
        {
            Orderproducts = new List<OrderProducts>();
        }

        public int OrderId { get; set; }
        public DateTime? Date { get; set; }
        public float? TotalPrice { get; set; }
        public string Address { get; set; }

        public virtual ICollection<OrderProducts> Orderproducts { get; set; }
    }/*
    public class Order
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public float TotalPrice { get; set; }
        public string Address { get; set; }
        public IList<OrderProducts> OrderProducts { get; set; }
    }*/
}
