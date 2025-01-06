using AutoMapper;
using EITracker.DbContext.Entities;
using EITracker.Models;

namespace EITracker.WebApi.Profiles
{
    public class LeaveRequestDetailProfile : Profile
    {
        public LeaveRequestDetailProfile()
        {
            this.CreateMap<LeaveRequestDetail, LeaveRequestDetailModel>()
                .ForAllMembers(opts => opts.Ignore());

            this.CreateMap<LeaveRequestDetail, LeaveRequestDetailModel>()
             .ReverseMap()
             .ForAllMembers(opts => opts.Ignore());

            this.CreateMap<LeaveRequestDetail, LeaveRequestDetailModel>()

                .ForMember(
                    model => model.LeaveId,
                    opts => opts.MapFrom(data => data.LeaveId))
                .ForMember(
                    model => model.LeaveRequestDetailId,
                    opts => opts.MapFrom(data => data.LeaveRequestDetailId))
                .ForMember(
                    model => model.AssignedTo,
                    opts => opts.MapFrom(data => data.AssignedTo))
                .ForMember(
                    model => model.DisplayOrder,
                    opts => opts.MapFrom(data => data.DisplayOrder))
                .ForMember(
                    model => model.CC,
                    opts => opts.MapFrom(data => data.CC))
                .ForMember(
                    model => model.Comments,
                    opts => opts.MapFrom(data => data.Comments))
                .ForMember(
                    model => model.Status,
                    opts => opts.MapFrom(data => data.Status))
                .ReverseMap()
               .ForMember(
                   data => data.LeaveId,
                   opts => opts.MapFrom(model => model.LeaveId))
               .ForMember(
                   data => data.LeaveRequestDetailId,
                   opts => opts.MapFrom(model => model.LeaveRequestDetailId))
               .ForMember(
                   data => data.AssignedTo,
                   opts => opts.MapFrom(model => model.AssignedTo))
               .ForMember(
                   data => data.DisplayOrder,
                   opts => opts.MapFrom(model => model.DisplayOrder))
               .ForMember(
                   data => data.CC,
                   opts => opts.MapFrom(model => model.CC))
               .ForMember(
                   data => data.Comments,
                   opts => opts.MapFrom(model => model.Comments))
               .ForMember(
                   data => data.Status,
                   opts => opts.MapFrom(model => model.Status));
        }
    }
}
