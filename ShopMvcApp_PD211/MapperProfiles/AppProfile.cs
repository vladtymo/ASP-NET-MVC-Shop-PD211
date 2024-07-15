using AutoMapper;
using ShopMvcApp_PD211.Dtos;
using ShopMvcApp_PD211.Entities;

namespace ShopMvcApp_PD211.MapperProfiles
{
    public class AppProfile : Profile
    {
        public AppProfile()
        {
            CreateMap<ProductDto, Product>().ReverseMap();
                //.ForMember(x => x.CategoryName, opt => opt.MapFrom(src => src.Category.Name));
        }
    }
}
