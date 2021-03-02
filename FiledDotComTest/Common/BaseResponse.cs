using FiledDotComTest.Domain.Dto.Response;
using System;
using System.Net;
using System.Threading.Tasks;

namespace FiledDotComTest.Common.Utility
{
    public class BaseResponse<T> : IBaseResponse<T>
    {
        public BaseResponse()
        {
        }
        public Task<ResponseObjectVM<T>> Success(object response = null, string msg = null)
        {
            try
            {
                var obj = new ResponseObjectVM<T>
                {
                    responseMessage = msg,
                    status = true,
                    statusCode = (int)HttpStatusCode.OK,
                    data = (T)response
                };
                return Task.FromResult(obj);
            }
            catch (Exception)
            {
                //Exception should be logged
                return null;
            }
        }

        public Task<ResponseObjectVM<T>> BadRequest(string msg = null)
        {
            try
            {
                var obj = new ResponseObjectVM<T>
                {
                    responseMessage = "The request is invalid. " + msg,
                    status = false,
                    statusCode = (int)HttpStatusCode.BadRequest
                };
                return Task.FromResult(obj);
            }
            catch (Exception)
            {
                //Exception should be logged
                return null;
            }
        }

        public Task<ResponseObjectVM<T>> InternalServerError(string msg = null)
        {
            try
            {
                var obj = new ResponseObjectVM<T>
                {
                    responseMessage = msg == null ? "Any Error" : msg,
                    status = false,
                    statusCode = (int)HttpStatusCode.InternalServerError
                };
                return Task.FromResult(obj);
            }
            catch (Exception)
            {
                //Exception should be logged
                return null;
            }
        }
    }
}
