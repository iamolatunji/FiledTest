using FiledDotComTest.Domain.Dto;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FiledDotComTest.Infrastructure
{
    public class PaymentApiService : IPaymentApiService
    {
        IBaseApiClient _apiCallImplementor;

        public PaymentApiService(IBaseApiClient apiCallImplementor)
        {
            _apiCallImplementor = apiCallImplementor;
        }

        public Task<bool> CheapPaymentGateway(ProcessPaymentRequest model)
        {
            try
            {
                //Make Api Call
                return Task.FromResult(true);
            }
            catch (Exception)
            {
                return Task.FromResult(false);
            }
        }

        public Task<bool> ExpensivePaymentGateway(ProcessPaymentRequest model)
        {
            try
            {
                //Make Api Call
                return Task.FromResult(true);
            }
            catch (Exception)
            {
                return Task.FromResult(false);
            }
        }

        public Task<bool> PremiumPaymentGateway(ProcessPaymentRequest model)
        {
            try
            {
                //Make Api Call
                return Task.FromResult(true);
            }
            catch (Exception)
            {
                return Task.FromResult(false);
            }
        }
    }
}
