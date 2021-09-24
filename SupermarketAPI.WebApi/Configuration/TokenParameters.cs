using SupermarketApi.Abstractions.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SupermarketApi.WebApi.Configuration
{
    public class TokenParameters : ITokenParameters
    {
        public string UserName { get; set; }
        public string PasswordHash { get; set; }
        public string Id { get ; set; }
    }
}
