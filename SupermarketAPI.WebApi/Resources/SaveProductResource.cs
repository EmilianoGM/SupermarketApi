using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SupermarketApi.WebApi.Resources
{
    public class SaveProductResource
    {
        [Required]
        [MaxLength(30)]
        public string Name { get; set; }
        [Required]
        public short QuantityInPackage { get; set; }
        [Required]
        [Range(1,5)]
        public int UnitOfMeasurement { get; set; }
        [Required]
        public int CategoryId { get; set; }
        [Required]
        public float Price { get; set; }
        [Required]
        [MaxLength(150)]
        public string ImgUrl { get; set; }
    }
}
