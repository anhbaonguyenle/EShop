using AutoMapper;
using EShop.ViewModels;
using EShop.Models;

namespace EShop.Helpers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<RegisterViewModel, CustomerModel>();//ForMember(cus => cus.CustomerFullName, option => option.MapFrom(RegisterViewModel => RegisterViewModel.CustomerFullName)).ReverseMap();
        }
    }
}
