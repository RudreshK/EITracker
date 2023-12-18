using AutoMapper;
using EITracker.DbContext.Entities;
using EITracker.Models;

namespace EITracker.Profiles
{
    public class CustomerProfile : Profile
    {
        public CustomerProfile()
        {
            this.CreateMap<Customer, CustomerModel>()
                .ForAllMembers(opts => opts.Ignore());

            this.CreateMap<Customer, CustomerModel>()
                .ReverseMap()
                .ForAllMembers(opts => opts.Ignore());

            this.CreateMap<Customer, CustomerModel>()
                .ForMember(
                    model => model.Id,
                    opts => opts.MapFrom(data => data.CustomerId))
                .ForMember(
                    model => model.CustomerName,
                    opts => opts.MapFrom(data => data.CustomerName))
                .ForMember(
                    model => model.ContactNumber,
                    opts => opts.MapFrom(data => data.ContactNumber))
                .ForMember(
                    model => model.Address,
                    opts => opts.MapFrom(data => data.Address))
                .ForMember(
                    model => model.Email,
                    opts => opts.MapFrom(data => data.Email))
                .ReverseMap()
                .ForMember(
                    data => data.CustomerId,
                    opts => opts.MapFrom(model => model.Id))
                .ForMember(
                    data => data.CustomerName,
                    opts => opts.MapFrom(model => model.CustomerName))
                .ForMember(
                    data => data.ContactNumber,
                    opts => opts.MapFrom(model => model.ContactNumber))
                .ForMember(
                    data => data.Address,
                    opts => opts.MapFrom(model => model.Address))
                .ForMember(
                    data => data.Email,
                    opts => opts.MapFrom(model => model.Email));
        }
    }
}
