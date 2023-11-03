using AutoMapper;
using EITracker.DbContext.Entities;

namespace EITracker.Models.Profiles
{
    public class EmployeeLeaveProfile : Profile
    {
        public EmployeeLeaveProfile()
        {
            this.CreateMap<EmployeeLeave, EmployeeLeaveModel>()
                .ForAllMembers(opts => opts.Ignore());

            this.CreateMap<EmployeeLeave, EmployeeLeaveModel>()
             .ReverseMap()
             .ForAllMembers(opts => opts.Ignore());

            this.CreateMap<EmployeeLeave, EmployeeLeaveModel>()
               .ForMember(model => model.EmployeeId, opts => opts.MapFrom(data => data.EmployeeId))
                .ForMember(
                   model => model.LeaveId,
                   opts => opts.MapFrom(data => data.LeaveId))
               .ForMember(
                   model => model.EmployeeId,
                   opts => opts.MapFrom(data => data.EmployeeId))
               .ForMember(
                   model => model.StartDate,
                   opts => opts.MapFrom(data => data.StartDate))
               .ForMember(
                   model => model.EndDate,
                   opts => opts.MapFrom(data => data.EndDate))
               .ForMember(
                   model => model.LeaveType,
                   opts => opts.MapFrom(data => data.LeaveType))
               .ForMember(
                   model => model.Status,
                   opts => opts.MapFrom(data => data.Status))
               .ForMember(
                   model => model.Comments,
                   opts => opts.MapFrom(data => data.Comments))

                .ReverseMap()
               .ForMember(
                   data => data.LeaveId,
                   opts => opts.MapFrom(model => model.LeaveId))
               .ForMember(
                   data => data.EmployeeId,
                   opts => opts.MapFrom(model => model.EmployeeId))
               .ForMember(
                   data => data.StartDate,
                   opts => opts.MapFrom(model => model.StartDate))
               .ForMember(
                   data => data.EndDate,
                   opts => opts.MapFrom(model => model.EndDate))
               .ForMember(
                   data => data.LeaveType,
                   opts => opts.MapFrom(model => model.LeaveType))
               .ForMember(
                   data => data.Status,
                   opts => opts.MapFrom(model => model.Status))
               .ForMember(
                   data => data.Comments,
                   opts => opts.MapFrom(model => model.Comments));
        }
    }
}
