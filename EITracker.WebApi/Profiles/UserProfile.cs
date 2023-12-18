// -----------------------------------------------------------------------------
// <copyright file="UserProfile.cs" company="ecoInsight, Inc.">
// Copyright © ecoInsight Inc. All rights reserved.
// </copyright>
// ------------------------------------------------------------------------------

using AutoMapper;
using EITracker.DbContext.Dbo;
using EITracker.Models;

namespace EITracker.Profiles
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

                //    .ForMember(
                //    model => model.CreatedById,
                //    opts => opts.MapFrom(data => data.CreatedById))
                //.ForMember(
                //    model => model.Created,
                //    opts => opts.MapFrom(data => (DateTimeOffset)data.CreatedTime))
                //.ForMember(
                //    model => model.ModifiedById,
                //    opts => opts.MapFrom(data => data.ModifiedById))
                //.ForMember(
                //    model => model.Modified,
                //    opts => opts.MapFrom(data => (DateTimeOffset)data.ModifiedTime))


                 .ReverseMap()
                //  .ForMember(
                //    data => data.CreatedById,
                //    opts => opts.MapFrom(model => model.CreatedById))
                //.ForMember(
                //    data => data.CreatedTime,
                //    opts => opts.MapFrom(model => model.Created.DateTime))
                //.ForMember(
                //    data => data.ModifiedById,
                //    opts => opts.MapFrom(model => model.ModifiedById))
                //.ForMember(
                //    data => data.ModifiedTime,
                //    opts => opts.MapFrom(model => model.Modified.DateTime))
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
