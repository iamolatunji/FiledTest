using FiledDotComTest.Domain.Dto;
using System.Threading.Tasks;

namespace FiledDotComTest.Infrastructure
{
    public interface IPaymentApiService
    {
        Task<bool> CheapPaymentGateway(ProcessPaymentRequest model);
        Task<bool> ExpensivePaymentGateway(ProcessPaymentRequest model);
        Task<bool> PremiumPaymentGateway(ProcessPaymentRequest model);
    }
}