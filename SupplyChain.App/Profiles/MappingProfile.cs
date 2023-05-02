using AutoMapper;
using SupplyChain.App.ViewModels;
using SupplyChain.Core.Models;

namespace SupplyChain.App.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            //Products
            CreateMap<Product, ProductViewModel>()
                .ForMember(dest => dest.CountryOfOriginList, opt => opt.Ignore())
                .ForMember(dest => dest.ManufacturerList, opt => opt.Ignore())
                .ForMember(dest => dest.CategoryList, opt => opt.Ignore())
                .ReverseMap();
            //Carts

        }
    }
}
