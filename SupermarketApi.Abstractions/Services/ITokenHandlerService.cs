﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupermarketApi.Abstractions.Services
{
    public interface ITokenHandlerService
    {
        string GenerateJwtToken(ITokenParameters pars);
    }
}
