using System.Net;

namespace FiledDotComTest.Domain.Dto.Response
{
    public class ResponseObjectVM<T>
    {
        public string responseMessage { get; set; }
        public bool status { get; set; }
        public int statusCode { get; set; }
        public T data { get; set; }
    }
}
