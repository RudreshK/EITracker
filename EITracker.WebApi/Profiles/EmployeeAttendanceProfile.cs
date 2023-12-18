using AutoMapper;
using EITracker.DbContext.Entities;
using EITracker.Models;

namespace EITracker.Profiles
{
    public class EmployeeAttendanceProfile : Profile
    {
        public EmployeeAttendanceProfile()
        {

            this.CreateMap<EmployeeAttendance, EmployeeAttendanceModel>()
                  .ForAllMembers(opts => opts.Ignore());

            this.CreateMap<EmployeeAttendance, EmployeeAttendanceModel>()
                .ReverseMap()
                .ForAllMembers(opts => opts.Ignore());

            this.CreateMap<EmployeeAttendance, EmployeeAttendanceModel>()
               .ForMember(model => model.EmployeeId, opts => opts.MapFrom(data => data.EmployeeId))
                .ForMember(
                   model => model.AttendanceId,
                   opts => opts.MapFrom(data => data.AttendanceId))
               .ForMember(
                   model => model.Date,
                   opts => opts.MapFrom(data => data.Date))
               .ForMember(
                   model => model.CheckIn,
                   opts => opts.MapFrom(data => data.CheckIn))
               .ForMember(
                   model => model.CheckOut,
                   opts => opts.MapFrom(data => data.CheckOut))
                        .ForMember(
                   model => model.Status,
                   opts => opts.MapFrom(data => data.Status))

                .ReverseMap()
               .ForMember(
                   data => data.AttendanceId,
                   opts => opts.MapFrom(model => model.AttendanceId))
               .ForMember(
                   data => data.EmployeeId,
                   opts => opts.MapFrom(model => model.EmployeeId))
               .ForMember(
                   data => data.Date,
                   opts => opts.MapFrom(model => model.Date))
               .ForMember(
                   data => data.CheckIn,
                   opts => opts.MapFrom(model => model.CheckIn))
               .ForMember(
                   data => data.CheckOut,
                   opts => opts.MapFrom(model => model.CheckOut))
               .ForMember(
                   data => data.Status,
                   opts => opts.MapFrom(model => model.Status));


        }
    }
}
