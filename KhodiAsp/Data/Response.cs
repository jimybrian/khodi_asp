using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KhodiAsp.Data
{
    public class Response<T>
    {
        public string responseMessage { get; set; }    
        public T response { get; set; }
    }
}