
using AutoMapper;
using EITracker.DbContext.Dbo;

namespace EITracker.Models.Profiles
{
    public class UserProfile : Profile
    {

        public UserProfile()
        {
            this.CreateMap<ApplicationUser, UserModel>()
                .ForAllMembers(opts => opts.Ignore());

            this.CreateMap<ApplicationUser, UserModel>()
                .ReverseMap()
                .ForAllMembers(opts => opts.Ignore());


            this.CreateMap<ApplicationUser, UserModel>()
                  .ForMember(model => model.Id, opts => opts.MapFrom(data => data.Id))
                .ForMember(model => model.UserId, opts => opts.MapFrom(data => data.UserId))
                 .ForMember(
                    model => model.FirstName,
                    opts => opts.MapFrom(data => data.FirstName))
                .ForMember(
                    model => model.LastName,
                    opts => opts.MapFrom(data => data.LastName))
                .ForMember(
                    model => model.Email,
                    opts => opts.MapFrom(data => data.Email))
                .ForMember(
                    model => model.PhoneNumber,
                    opts => opts.MapFrom(data => data.PhoneNumber))
                 .ReverseMap()
                  .ForMember(
                    data => data.Id,
                    opts => opts.MapFrom(model => model.Id))
                .ForMember(
                    data => data.UserId,
                    opts => opts.MapFrom(model => model.UserId))
                .ForMember(
                    data => data.FirstName,
                    opts => opts.MapFrom(model => model.FirstName))
                .ForMember(
                    data => data.LastName,
                    opts => opts.MapFrom(model => model.LastName))
                                .ForMember(
                    data => data.PhoneNumber,
                    opts => opts.MapFrom(model => model.PhoneNumber))
                .ForMember(
                    data => data.Email,
                    opts => opts.MapFrom(model => model.Email));
        }
    }
}
