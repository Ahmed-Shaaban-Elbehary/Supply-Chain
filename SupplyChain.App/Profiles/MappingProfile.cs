using AutoMapper;
using SupplyChain.App.ViewModels;
using SupplyChain.Core.Models;

namespace SupplyChain.App.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Product, ProductViewModel>().ReverseMap();
            CreateMap<ProductCategory, ProductCategoryViewModel>().ReverseMap();
            CreateMap<Manufacturer, ManufacturerViewModel>().ReverseMap();
            CreateMap<User, UserViewModel>().ReverseMap();
            CreateMap<Role, RoleViewModel>().ReverseMap();
        }
    }
}
