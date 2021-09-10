using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SupermarketAPI.Resources
{
    /*
     * A resource class contains only basic information that will be exchanged between client applications 
     * and API endpoints, generally in form of JSON data.
     */
    public class CategoryResource
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
