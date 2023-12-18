using AutoMapper;
using EITracker.DbContext.Entities;
using EITracker.Models;

namespace EITracker.Profiles
{
    public class AuthorProfile : Profile
    {
        public AuthorProfile()
        {
            this.CreateMap<Author, AuthorModel>()
                .ForAllMembers(opts => opts.Ignore());

            this.CreateMap<Author, AuthorModel>()
                .ReverseMap()
                .ForAllMembers(opts => opts.Ignore());

            this.CreateMap<Author, AuthorModel>()
                .ForMember(
                    model => model.Id,
                    opts => opts.MapFrom(data => data.AuthorId))
                .ForMember(
                    model => model.AuthorName,
                    opts => opts.MapFrom(data => data.Name))
                .ForMember(
                    model => model.Address,
                    opts => opts.MapFrom(data => data.Address))
                .ForMember(
                    model => model.Qualification,
                    opts => opts.MapFrom(data => data.Qualification))
                .ForMember(
                    model => model.Books,
                    opts => 
                    {
                        opts.MapFrom(data => data.Books);
                        opts.ExplicitExpansion();
                    })
                .ReverseMap()
                .ForMember(
                    data => data.AuthorId,
                    opts => opts.MapFrom(model => model.Id))
                .ForMember(
                    data => data.Name,
                    opts => opts.MapFrom(model => model.AuthorName))
                .ForMember(
                    data => data.Address,
                    opts => opts.MapFrom(model => model.Address))
                .ForMember(
                    data => data.Qualification,
                    opts => opts.MapFrom(model => model.Qualification));
        }
    }
}
