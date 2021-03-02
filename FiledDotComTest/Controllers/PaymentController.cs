using FiledDotComTest.Common.Utility;
using FiledDotComTest.Core.Application;
using FiledDotComTest.Domain.Dto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FiledDotComTest.Controllers
{
    [ApiController]
    [Route("Api/[controller]")]
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentService _paymentService;
        private readonly IBaseResponse<object> _baseResponse;
        public PaymentController(IPaymentService paymentService, IBaseResponse<object> baseResponse)
        {
            _paymentService = paymentService;
            _baseResponse = baseResponse;
        }

        [HttpPost]
        public async Task<IActionResult> ProcessPayment([FromBody] ProcessPaymentRequest model)
        {
            if (!ModelState.IsValid)
            {
                string messages = string.Join("; ", ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage));
                return StatusCode(400, await _baseResponse.BadRequest(messages));
            }
            var response = await _paymentService.ProcessPayment(model);
            return StatusCode((int)response.statusCode, response);
        }


    }
}
