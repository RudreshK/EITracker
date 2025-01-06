using AutoMapper;
using EITracker.DbContext.Entities;
using EITracker.Models;

namespace EITracker.Profiles
{
    public class LeaveRequestProfile : Profile
    {
        public LeaveRequestProfile()
        {
            this.CreateMap<LeaveRequest, LeaveRequestModel>()
                .ForAllMembers(opts => opts.Ignore());

            this.CreateMap<LeaveRequest, LeaveRequestModel>()
             .ReverseMap()
             .ForAllMembers(opts => opts.Ignore());

            this.CreateMap<LeaveRequest, LeaveRequestModel>()

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
                   opts => opts.MapFrom(data => (Enums.LeaveType)data.LeaveType))
               .ForMember(
                   model => model.Status,
                   opts => opts.MapFrom(data => (Enums.LeaveStatus)data.Status))
               .ForMember(
                   model => model.Message,
                   opts => opts.MapFrom(data => data.Message))
               .ForMember(
                   model => model.NoOfDays,
                   opts => opts.MapFrom(data => data.NoOfDays))
                .ForMember(
                   model => model.Subject,
                   opts => opts.MapFrom(data => data.Subject))
                 .ForMember(
                   model => model.Attachments,
                   opts => opts.MapFrom(data => data.Attachments))

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
                   opts => opts.MapFrom(model => (byte)model.LeaveType))
               .ForMember(
                   data => data.Status,
                   opts => opts.MapFrom(model => (byte)model.Status))
               .ForMember(
                   data => data.Message,
                   opts => opts.MapFrom(model => model.Message))
               .ForMember(
                   data => data.NoOfDays,
                   opts => opts.MapFrom(model => model.NoOfDays))
               .ForMember(
                   data => data.Subject,
                   opts => opts.MapFrom(model => model.Subject))
               .ForMember(
                   data => data.Attachments,
                   opts => opts.MapFrom(model => model.Attachments))
               ;
        }
    }
}
