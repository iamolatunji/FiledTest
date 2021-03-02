using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace FiledDotComTest.Domain.Dto
{
    public class ApiResponseObject
    {
        public string responseContent { get; set; }
        public HttpStatusCode statusCode { get; set; }
        public object StatusCode { get; set; }
    }
}
