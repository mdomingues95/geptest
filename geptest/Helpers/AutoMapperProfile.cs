using AutoMapper;
using geptest.Models;

namespace geptest.Helpers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            // CreateRequest -> User
            CreateMap<ProductRequest, Product>();
        }
    }
}
