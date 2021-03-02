using AutoMapper;
using FiledDotComTest.Domain.Dto;
using FiledDotComTest.Domain.Entities;

namespace Cooperative.Common.Utility
{
    public class AutoMapperProfile : Profile
    {
        // mappings between model and entity objects
        public AutoMapperProfile()
        {
            CreateMap<Payment, ProcessPaymentRequest>().ReverseMap();
        }
    }
}
