using SupermarketApi.Abstractions.Communication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SupermarketApi.Application.Communication
{
    public class ResponseWrapper<T> : IResponseWrapper<T>
    {
        public bool Succeeded { get; set; }
        public string Message { get; set; }
        public string Errors { get; set; }
        public T Data { get; set; }

        public ResponseWrapper()
        {

        }

        public ResponseWrapper(T data)
        {
            Succeeded = true;
            Message = String.Empty;
            Errors = String.Empty;
            Data = data;
        }

        public ResponseWrapper(string message, string errors)
        {
            Succeeded = false;
            Message = message;
            Errors = errors;
            Data = default;
        }

    }
}
