using SupermarketAPI.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SupermarketAPI.Domain.Services.Communication
{
    public class ProductResponse : BaseResponse<Product>
    {
        /// <summary>
        /// Creates a succes response.
        /// </summary>
        /// <param name="product">Resulted product</param>
        public ProductResponse(Product product): base(product) { }

        /// <summary>
        /// Creates an error response.
        /// </summary>
        /// <param name="message">Error message</param>
        public ProductResponse(string message) : base(message) { }
    }
}
