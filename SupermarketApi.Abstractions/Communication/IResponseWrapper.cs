﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupermarketApi.Abstractions.Communication
{
    public interface IResponseWrapper<T>
    {
        public bool Succeeded { get; set; }
        public string Message { get; set; }
        public string Errors { get; set; }
        public T Data { get; set; }
    }
}
