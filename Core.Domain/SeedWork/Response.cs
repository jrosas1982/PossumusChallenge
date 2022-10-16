using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Domain.SeedWork
{
    public class Response<T>
    {
        public T Data { get; set; }
        public bool IsSuccess { get; set; } = false;
        public string Message { get; set; }
    }
}
