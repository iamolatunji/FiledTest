using AutoMapper;
using FiledDotComTest.Common.Utility;
using FiledDotComTest.Core.Application;
using FiledDotComTest.Domain.Dto;
using FiledDotComTest.Infrastructure;
using FiledDotComTest.Persistence.DbConnection;
using FiledDotComTest.Persistence.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace Test
{
    public class PaymentServiceTest
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public async Task TestPaymentService()
        {
            var serviceProvider = new ServiceCollection()
                .AddDbContext<FiledDbContext>(options => options.UseSqlServer("Data Source=localhost;Initial Catalog=FiledTestDb;Integrated Security=false;uid=SA;pwd=instiq2021Hr"))
                .AddScoped<PaymentRepository>()
                .AddScoped<PaymentStateRepository>()
                .AddScoped<IBaseApiClient, BaseApiClient>()
                .AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies())
                .AddScoped<IPaymentApiService, PaymentApiService>()
                .AddScoped(typeof(IBaseResponse<>), typeof(BaseResponse<>))
                .BuildServiceProvider();
            var paymentRepo = serviceProvider.GetService<PaymentRepository>();
            var paymentStateRepo = serviceProvider.GetService<PaymentStateRepository>();
            var paymentApiService = serviceProvider.GetService<IPaymentApiService>();
            var mapper = serviceProvider.GetService<IMapper>();
            var baseResponse = serviceProvider.GetService<IBaseResponse<object>>();
            PaymentService payment = new PaymentService(paymentRepo, paymentStateRepo, mapper, baseResponse, paymentApiService);
            var result = await payment.ProcessPayment(new ProcessPaymentRequest { 
                CreditCardNumber = "142435353636646646",
                CardHolder = "Andrew Surname",
                ExpirationDate = DateTime.Now.AddDays(5),
                SecurityCode = "656",
                Amount = 200.00M
            });
            Assert.AreEqual("Payment is processed", result.responseMessage);
        }
    }
}