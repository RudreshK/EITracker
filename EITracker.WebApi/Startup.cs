// <copyright file="Startup.cs" company="EcoInsight">
// Copyright (c) EcoInsight. All rights reserved.
// </copyright>

using System.Reflection;
using System.Text.Json;
using AutoMapper;
using EITracker.DbContext;
using EITracker.DbContext.Dbo;
using EITracker.Models;
using EITracker.Profiles;
using Microsoft.AspNet.OData.Builder;
using Microsoft.AspNet.OData.Extensions;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.OData.Edm;
using ODataDemo.Services;


namespace EITracker.WebApi
{
    public class Startup
    {

        public static IConfiguration Configuration { get; private set; }
        public static IConfiguration StaticConfiguration { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Startup"/> class.
        /// </summary>
        /// <param name="configuration">IConfiguration.</param>
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            StaticConfiguration = configuration;
        }
        /// <summary>
        /// This method gets called by the runtime. Use this method to add services to the container.
        /// </summary>
        /// <param name="services">IServiceCollection.</param>
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<LibraryDbContext>(
                options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddDbContext<ApplicationDbContext>(
                options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddIdentityCore<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
              .AddRoles<ApplicationRole>()
              .AddEntityFrameworkStores<ApplicationDbContext>()
              .AddDefaultTokenProviders();

            services.Configure<DataProtectionTokenProviderOptions>(opt => opt.TokenLifespan = TimeSpan.FromHours(24));
            services.Configure<IdentityOptions>(options =>
            {
                // Password settings.
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequiredLength = 5;
                options.Password.RequiredUniqueChars = 1;

                // Lockout settings.
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.AllowedForNewUsers = true;

                // User settings.
                options.User.AllowedUserNameCharacters =
                "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
                options.User.RequireUniqueEmail = false;
            });
            services.AddCors(options =>
            {
                string origins = Configuration.GetSection("AllowedOrigins")?.Value;

                if (!string.IsNullOrEmpty(origins))
                {
                    // https://docs.microsoft.com/en-us/aspnet/core/security/cors?view=aspnetcore-5.0
                    string[] originList = origins.Split(new char[] { ',', ' ' }, StringSplitOptions.RemoveEmptyEntries);
                    options.AddDefaultPolicy(
                    builder =>
                    {
                        builder.WithOrigins(originList)
                            .AllowCredentials()
                            .AllowAnyMethod()
                            .AllowAnyHeader();
                    });
                }
            });
            services.AddControllers();
            // Auto Mapper Configurations
            var mapperConfig = new MapperConfiguration(mc =>
            {
                Assembly assembly = Assembly.GetExecutingAssembly();
                var nameSpace1 = typeof(AuthorProfile).Namespace;

                var profileTypes = assembly.GetTypes()
                    .Where(w => (string.Equals(w.Namespace, nameSpace1, StringComparison.InvariantCultureIgnoreCase)) &&
                                w.Name.EndsWith("Profile"))
                    .ToList();

                foreach (var type in profileTypes)
                    mc.AddProfile((Profile)Activator.CreateInstance(type));
            });
            IMapper mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);
            services.AddSingleton<ITypeMapper, TypeMapper>();
            services.AddScoped<UserManager<ApplicationUser>>();
            services.AddSingleton<JwtService>(new JwtService("your-secret-key"));
            services.AddMvc(option => option.EnableEndpointRouting = false);
            services.AddOData();


        }

        /// <summary>
        /// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// </summary>
        /// <param name="app">IApplicationBuilder.</param>
        /// <param name="env">IWebHostEnvironment.</param>

        public void Configure(WebApplication app, IWebHostEnvironment env)
        {
            app.UseExceptionHandler(errorApp =>
            {
                errorApp.Run(async context =>
                {
                    var handlerFeature = context.Features.Get<IExceptionHandlerFeature>();
                    if (handlerFeature != null && handlerFeature.Error != null & context.Response.StatusCode >= 500 && context.Response.StatusCode < 600)
                    {
                        var response = new { Message = handlerFeature.Error.Message, StackTrace = handlerFeature.Error.StackTrace, InnerException = handlerFeature.Error.InnerException?.Message };
                        context.Response.Headers.Add("Content-Type", "application/json; odata.metadata=minimal; odata.streaming=true");
                        await context.Response.WriteAsync(JsonSerializer.Serialize(response));
                    }
                });
            });

            app.MapGet("/", () => "Blazor WEBAPI Demo");
            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
                endpoints.MapControllers();
            }); 
            app.UseMvc(options =>
            {
                var model = GetEdmModel();
                options.Expand().Select().Filter().Count().OrderBy();

                options.MapODataServiceRoute("ODataRoute", "api", GetEdmModel());
                options.EnableDependencyInjection();
            });
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseCors();
            app.Run();
        }

        /// <summary>
        /// Get EDM model for ODataRoute.
        /// </summary>
        /// <returns>IEdmModel.</returns>
        private static IEdmModel GetEdmModel()
        {
            ODataConventionModelBuilder builder = new ODataConventionModelBuilder();
            builder.EntitySet<AuthorModel>("Authors");
            builder.EntitySet<CustomerModel>("Customers");
            builder.EntitySet<ProductModel>("Products");
            builder.EntitySet<UserModel>("Users").EntityType.Action("Roles");
            var roles = builder.Action("Roles");
            var AppUser = builder.EntitySet<UserModel>("Users");
            AppUser.EntityType.Action("PostUser");
            AppUser.EntityType.Action("GetAllUsers");

            return builder.GetEdmModel();
        }
    }
}
