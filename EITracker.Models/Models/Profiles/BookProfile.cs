using AutoMapper;
using EITracker.DbContext.Entities;

namespace EITracker.Models.Profiles
{
    public class BookProfile : Profile
    {
        public BookProfile()
        {
            this.CreateMap<Book, BookModel>()
                .ForAllMembers(opts => opts.Ignore());

            this.CreateMap<Book, BookModel>()
                .ReverseMap()
                .ForAllMembers(opts => opts.Ignore());

            this.CreateMap<Book, BookModel>()
                .ForMember(
                    model => model.Id,
                    opts => opts.MapFrom(data => data.BookId))
                .ForMember(
                    model => model.Title,
                    opts => opts.MapFrom(data => data.Title))
                .ForMember(
                    model => model.Publication,
                    opts => opts.MapFrom(data => data.Publication))
                .ForMember(
                    model => model.PublicationYear,
                    opts => opts.MapFrom(data => data.PublicationYear))
                .ForMember(
                    model => model.ISBN,
                    opts => opts.MapFrom(data => data.ISBN))
                .ForMember(
                    model => model.Author,
                    opts =>
                    {
                        opts.MapFrom(data => data.Author);
                        opts.ExplicitExpansion();
                    })
                .ReverseMap()
                .ForMember(
                    data => data.BookId,
                    opts => opts.MapFrom(model => model.Id))
                .ReverseMap()
                .ForMember(
                    data => data.Title,
                    opts => opts.MapFrom(model => model.Title))
                .ReverseMap()
                .ForMember(
                    data => data.Publication,
                    opts => opts.MapFrom(model => model.Publication))
                .ReverseMap()
                .ForMember(
                    data => data.PublicationYear,
                    opts => opts.MapFrom(model => model.PublicationYear))
                .ReverseMap()
                .ForMember(
                    data => data.ISBN,
                    opts => opts.MapFrom(model => model.ISBN));
        }
    }
}
