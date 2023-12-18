// <copyright file="Program.cs" company="EcoInsight">
// Copyright (c) EcoInsight. All rights reserved.
// </copyright>

using EITracker.WebApi;

var builder = WebApplication.CreateBuilder(args);

Startup applicationStartup = new Startup(builder.Configuration);
applicationStartup.ConfigureServices(builder.Services);
applicationStartup.Configure(builder.Build(), builder.Environment);


//using AutoMapper;
//using BlazorChat.Server;
//using EITracker.DbContext;
//using EITracker.DbContext.Dbo;
//using EITracker.Models;
//using EITracker.Models.Profiles;
//using Microsoft.AspNet.OData.Builder;
//using Microsoft.AspNet.OData.Extensions;
//using Microsoft.AspNetCore.Identity;
//using Microsoft.EntityFrameworkCore;
//using ODataDemo.Services;

//var builder = WebApplication.CreateBuilder(args);
//builder.Services
//    .AddEntityFrameworkSqlServer()
//    .AddDbContext<LibraryDbContext>(
//        options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

//builder.Services.AddDbContext<ApplicationDbContext>(
//              options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


//builder.Services.AddIdentityCore<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
//             .AddRoles<ApplicationRole>()
//             .AddEntityFrameworkStores<ApplicationDbContext>()
//             .AddDefaultTokenProviders();

//builder.Services.Configure<DataProtectionTokenProviderOptions>(opt => opt.TokenLifespan = TimeSpan.FromHours(24));
//builder.Services.AddSignalR();
//builder.Services.Configure<IdentityOptions>(options =>
//{
//    // Password settings.
//    options.Password.RequireDigit = false;
//    options.Password.RequireLowercase = false;
//    options.Password.RequireNonAlphanumeric = false;
//    options.Password.RequireUppercase = false;
//    options.Password.RequiredLength = 5;
//    options.Password.RequiredUniqueChars = 1;

//    // Lockout settings.
//    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
//    options.Lockout.MaxFailedAccessAttempts = 5;
//    options.Lockout.AllowedForNewUsers = true;

//    // User settings.
//    options.User.AllowedUserNameCharacters =
//    "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
//    options.User.RequireUniqueEmail = false;
//});

//var mapperConfig = new MapperConfiguration(mc =>
//{
//    mc.AddProfile(new AuthorProfile());
//    mc.AddProfile(new BookProfile());
//    mc.AddProfile(new CustomerProfile());
//    mc.AddProfile(new ProductProfile());
//    mc.AddProfile(new UserProfile());
//    mc.AddProfile(new EmployeeAttendanceProfile());
//    mc.AddProfile(new EmployeeLeaveProfile());
//});
//IMapper mapper = mapperConfig.CreateMapper();
//builder.Services.AddSingleton(mapper);
//builder.Services.AddSingleton<ITypeMapper, TypeMapper>();
//builder.Services.AddScoped<UserManager<ApplicationUser>>();
//builder.Services.AddMvc(option => option.EnableEndpointRouting = false);
//builder.Services.AddOData();

//var app = builder.Build();
//app.MapGet("/", () => "Blazor WEBAPI Demo");
//app.UseRouting();
//app.UseEndpoints(endpoints =>
//{
//    endpoints.MapRazorPages();
//    endpoints.MapControllers();
//    endpoints.MapHub<SignalRHub>("/signalRHub");
//});
//app.UseMvc(options =>
//{
//    ODataConventionModelBuilder builder = new ODataConventionModelBuilder();
//    builder.EntitySet<AuthorModel>("Authors");
//    builder.EntitySet<CustomerModel>("Customers");
//    builder.EntitySet<ProductModel>("Products");
//    builder.EntitySet<UserModel>("Users").EntityType.Action("Roles");
//    var roles = builder.Action("Roles");
//    var AppUser = builder.EntitySet<UserModel>("Users");
//    AppUser.EntityType.Action("PostUser");
//    AppUser.EntityType.Action("GetAllUsers");

//    var edmModel = builder.GetEdmModel();
//    options.Expand().Select().Filter().Count().OrderBy();
//    options.MapODataServiceRoute("RouteServices", "api", edmModel);
//});
//app.Run();
