using FiledDotComTest.Domain.Dto;
using FiledDotComTest.Domain.Dto.Response;
using System.Threading.Tasks;

namespace FiledDotComTest.Core.Application
{
    public interface IPaymentService
    {
        Task<ResponseObjectVM<object>> ProcessPayment(ProcessPaymentRequest model);
    }
}