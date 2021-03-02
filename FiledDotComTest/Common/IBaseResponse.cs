using FiledDotComTest.Domain.Dto.Response;
using System.Threading.Tasks;

namespace FiledDotComTest.Common.Utility
{
    public interface IBaseResponse<T>
    {
        Task<ResponseObjectVM<T>> Success(object response = null, string msg = null);
        Task<ResponseObjectVM<T>> BadRequest(string msg = null);
        Task<ResponseObjectVM<T>> InternalServerError(string msg = null);
    }
}