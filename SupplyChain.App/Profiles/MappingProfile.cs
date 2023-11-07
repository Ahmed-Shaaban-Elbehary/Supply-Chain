using AutoMapper;
using SupplyChain.App.ViewModels;
using SupplyChain.Core.Models;

namespace SupplyChain.App.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Product, ProductViewModel>()
                .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.Name))
                .ReverseMap();

            CreateMap<ProductCategory, ProductCategoryViewModel>().ReverseMap();

            CreateMap<Manufacturer, ManufacturerViewModel>().ReverseMap();


            CreateMap<User, UserViewModel>()
                .ForMember(dest => dest.Username, opt => opt.MapFrom(src => src.Name))
                .ReverseMap();

            CreateMap<RoleViewModel, Role>()
                .ForMember(dest => dest.UserRoles, opt => opt.Ignore())
                .ForMember(dest => dest.RolePermissions, opt => opt.Ignore())
                .ReverseMap();

            CreateMap<Event, EventViewModel>()
            .ForMember(dest => dest.Users, opt => opt.MapFrom(src => src.UserEvents))
            .ForMember(dest => dest.Products, opt => opt.MapFrom(src => src.ProductEvents))
            .ReverseMap();

            CreateMap<EventStatus, EventStatusViewModel>().ReverseMap();

            CreateMap<ProductQuantityRequest, ProductQuantityRequestViewModel>()
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString()))
            //.ForMember(dest => dest.EventId, opt => opt.MapFrom(src => src.AssociatedEvent.Id))
            //.ForMember(dest => dest.EventName, opt => opt.MapFrom(src => src.AssociatedEvent.Title))
            .ForMember(dest => dest.ProductId, opt => opt.MapFrom(src => src.Product.Id))
            .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.Product.Name))
            .ForMember(dest => dest.UnitCode, opt => opt.MapFrom(src => src.Product.UnitCode));

            CreateMap<ProductQuantityRequestViewModel, ProductQuantityRequest>()
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => Enum.Parse(typeof(RequestStatus), src.Status)));
        }
    }
}
