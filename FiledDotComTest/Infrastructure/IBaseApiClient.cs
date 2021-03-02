using FiledDotComTest.Domain.Dto;
using System;
using System.Threading.Tasks;

namespace FiledDotComTest.Infrastructure
{
    public interface IBaseApiClient
    {
        Task<ApiResponseObject> PostApiService(string baseurl, string apiurl, Object model, double requestTimeOut = 05);
        Task<ApiResponseObject> GetApiService(string baseurl, string apiurlAndParam, double requestTimeOut = 05);
    }
}