using AutoMapper;
using EITracker.DbContext.Entities;

namespace EITracker.Models.Profiles
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            this.CreateMap<Product, ProductModel>()
                .ForAllMembers(opts => opts.Ignore());

            this.CreateMap<Product, ProductModel>()
                .ReverseMap()
                .ForAllMembers(opts => opts.Ignore());

            this.CreateMap<Product, ProductModel>()
                .ForMember(model => model.Id, opts => opts.MapFrom(data => data.Id))
                 .ForMember(
                    model => model.ProductName,
                    opts => opts.MapFrom(data => data.ProductName))
                .ForMember(
                    model => model.Brand,
                    opts => opts.MapFrom(data => data.Brand))
                .ForMember(
                    model => model.Price,
                    opts => opts.MapFrom(data => data.Price))
                 .ReverseMap()
                .ForMember(
                    data => data.Id,
                    opts => opts.MapFrom(model => model.Id))
                .ForMember(
                    data => data.ProductName,
                    opts => opts.MapFrom(model => model.ProductName))
                .ForMember(
                    data => data.Brand,
                    opts => opts.MapFrom(model => model.Brand))
                .ForMember(
                    data => data.Price,
                    opts => opts.MapFrom(model => model.Price));
        }

    }
}
