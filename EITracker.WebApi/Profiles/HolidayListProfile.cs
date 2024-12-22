using AutoMapper;
using DBEntities = EITracker.DbContext.Entities;

namespace EITracker.Profiles
{
    public class HolidayListProfile : Profile
    {
        public HolidayListProfile()
        {
            this.CreateMap<DBEntities.HolidayList, Models.HolidayListModel>()
               .ForAllMembers(opts => opts.Ignore());

            this.CreateMap<DBEntities.HolidayList, Models.HolidayListModel>()
                .ReverseMap()
                .ForAllMembers(opts => opts.Ignore());

            this.CreateMap<DBEntities.HolidayList, Models.HolidayListModel>()
               .ForMember(model => model.HolidayId, opts => opts.MapFrom(data => data.HolidayId))
                .ForMember(
                   model => model.HolidayName,
                   opts => opts.MapFrom(data => data.HolidayName))
               .ForMember(
                   model => model.HolidayDate,
                   opts => opts.MapFrom(data => data.HolidayDate))
                .ForMember(
                   model => model.WeekDay,
                   opts => opts.MapFrom(data => (Enums.WeekDays)data.WeekDay))

               .ReverseMap()
               .ForMember(
                   data => data.HolidayId,
                   opts => opts.MapFrom(model => model.HolidayId))
               .ForMember(
                   data => data.HolidayName,
                   opts => opts.MapFrom(model => model.HolidayName))
               .ForMember(
                   data => data.HolidayDate,
                   opts => opts.MapFrom(model => model.HolidayDate))
               .ForMember(
                   data => data.WeekDay,
                   opts => opts.MapFrom(model => (byte)model.WeekDay))
              ;

        }
    }
}
