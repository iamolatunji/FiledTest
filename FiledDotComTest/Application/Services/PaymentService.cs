using AutoMapper;
using Microsoft.Extensions.Options;
using System;
using System.Threading.Tasks;
using FiledDotComTest.Domain.Dto;
using FiledDotComTest.Common.Utility;
using FiledDotComTest.Persistence.Repository;
using FiledDotComTest.Domain.Dto.Response;
using FiledDotComTest.Domain.Entities;
using FiledDotComTest.Domain.Enum;
using FiledDotComTest.Infrastructure;

namespace FiledDotComTest.Core.Application
{
    public class PaymentService : IPaymentService
    {
        private readonly PaymentRepository _paymentRepository;
        private readonly PaymentStateRepository _paymentStateRepository;
        private readonly IBaseResponse<object> _baseResponse;
        private readonly IPaymentApiService _paymentApiService;
        IMapper _mapper;
        public PaymentService(
            PaymentRepository paymentRepository,
            PaymentStateRepository paymentStateRepository,
            IMapper mapper,
            IBaseResponse<object> baseResponse,
            IPaymentApiService paymentApiService)
        {
            _paymentRepository = paymentRepository;
            _paymentStateRepository = paymentStateRepository;
            _mapper = mapper;
            _baseResponse = baseResponse;
            _paymentApiService = paymentApiService;
        }

        public async Task<ResponseObjectVM<object>> ProcessPayment(ProcessPaymentRequest model)
        {
            using (var transaction = await _paymentRepository.InitiateTransaction())
            {
                try
                {
                    // map model to new Payment object
                    var paymentRequest = _mapper.Map<Payment>(model);
                    paymentRequest.PaymentState = new PaymentState { State = Status.Pending, PaymentId = paymentRequest.Id};

                    // save Payment
                    var savePayment = await _paymentRepository.Add(paymentRequest);
                    if (savePayment == null)
                    {
                        await transaction.RollbackAsync();
                        return await _baseResponse.InternalServerError("Payment process failed.");
                    }

                    //Perform payment through gateway
                    if (await ProcessThroughPaymentGateway(model))
                    {
                        var paymentState = savePayment.PaymentState;
                        paymentState.State = Status.Processed;
                        var updatePaymentState = await _paymentStateRepository.Update(paymentState);
                        if(updatePaymentState == null)
                        {
                            await transaction.RollbackAsync();
                            return await _baseResponse.InternalServerError("Payment processing failed. Failed to update payment state.");
                        }
                    }
                    else
                    {
                        var paymentState = savePayment.PaymentState;
                        paymentState.State = Status.Failed;
                        var updatePaymentState = await _paymentStateRepository.Update(paymentState);
                        if (updatePaymentState == null)
                        {
                            await transaction.RollbackAsync();
                            return await _baseResponse.InternalServerError("Payment processing failed. Failed to update payment state.");
                        }
                        await transaction.RollbackAsync();
                        return await _baseResponse.InternalServerError("Payment processing failed.");
                    }
                    
                    await transaction.CommitAsync();
                    return await _baseResponse.Success(null, "Payment is processed");
                }
                catch (Exception)
                {
                    await transaction.RollbackAsync();
                    return await _baseResponse.InternalServerError();
                }
            }
        }

        private async Task<bool> ProcessThroughPaymentGateway(ProcessPaymentRequest model)
        {
            //For amount below 20 pounds
            if (model.Amount < 20)
                return await _paymentApiService.CheapPaymentGateway(model);

            //For amount between 20 and 500 pounds
            if(model.Amount > 20 & model.Amount <= 500)
            {
                var useExpensive = await _paymentApiService.ExpensivePaymentGateway(model);
                if(useExpensive == false)
                    return await _paymentApiService.CheapPaymentGateway(model);
                return true;
            }
            
            //For amount above 500 pounds
            if(model.Amount > 500)
            {
                bool response = true;
                response = await _paymentApiService.PremiumPaymentGateway(model);
                if(response == false)
                    response = await _paymentApiService.PremiumPaymentGateway(model);
                if (response == false)
                    response = await _paymentApiService.PremiumPaymentGateway(model);
                if (response == false)
                    response = await _paymentApiService.PremiumPaymentGateway(model);
                return response;
            }
            return false;
        }
    }
}
