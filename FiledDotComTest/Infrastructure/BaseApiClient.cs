using FiledDotComTest.Domain.Dto;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace FiledDotComTest.Infrastructure
{
    public class BaseApiClient : IBaseApiClient
    {
        public BaseApiClient()
        {
        }

        public async Task<ApiResponseObject> PostApiService(string baseurl, string apiurl, Object model, double requestTimeOut = 05)
        {
            ApiResponseObject res = new ApiResponseObject();
            try
            {
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(baseurl);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.Timeout = TimeSpan.FromSeconds(requestTimeOut);
                var iToken = String.Empty;
                if (iToken != String.Empty && iToken != null)
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", iToken);
                var data = JsonConvert.SerializeObject(model);
                var content = new StringContent(data, Encoding.UTF8, "application/json");
                var response = await client.PostAsync(apiurl, content);
                if (response.StatusCode == HttpStatusCode.OK && response.Content != null)
                {
                    res.statusCode = response.StatusCode;
                    res.responseContent = await response.Content.ReadAsStringAsync();
                }
                else
                {
                    res.statusCode = response.StatusCode;
                    res.responseContent = await response.Content.ReadAsStringAsync();
                }
                return res;
            }
            catch (Exception)
            {
                res.statusCode = HttpStatusCode.InternalServerError;
                return res;
            }
        }

        public async Task<ApiResponseObject> GetApiService(string baseurl, string apiurlAndParam, double requestTimeOut = 05)
        {
            ApiResponseObject res = new ApiResponseObject();
            try
            {
                string uri = baseurl + apiurlAndParam;
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(uri);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.Timeout = TimeSpan.FromSeconds(requestTimeOut);
                var iToken = String.Empty;
                if (iToken != String.Empty)
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", iToken);
                var response = await client.GetAsync(uri);
                if (response.StatusCode == HttpStatusCode.OK && response.Content != null)
                {
                    res.statusCode = response.StatusCode;
                    res.responseContent = response.Content.ReadAsStringAsync().Result;
                }
                else
                {
                    res.statusCode = response.StatusCode;
                    res.responseContent = await response.Content.ReadAsStringAsync();
                }
                return res;
            }
            catch (Exception)
            {
                res.statusCode = HttpStatusCode.InternalServerError;
                return res;
            }
        }
    }
}
