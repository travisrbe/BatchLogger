using ApiServer.Middleware;
using Domain.Entities;
using Domain.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Persistence;
using Persistence.Repositories;
using Service.Abstractions;
using Services;

namespace ApiServer.Extensions
{
    public static class ServiceExtensions
    {
        public static void ConfigureCors(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder
                    .AllowAnyOrigin()
                    .AllowAnyHeader()
                    .AllowAnyMethod());
            });
        }

        public static void ConfigureIISIntegration(this IServiceCollection services)
        {
            services.Configure<IISOptions>(options =>
            {

            });

        }

        public static void ConfigureSqlContext(this IServiceCollection services, IConfiguration config)
        {
            var connectionString = config["ConnectionStrings:Development"];
            services.AddDbContext<DataContext>(o => o.UseSqlServer(connectionString));
        }

        public static void ConfigureIdentity(this IServiceCollection services, IConfiguration config)
        {
            var builder = services.AddIdentityCore<User>(o =>
            {
                o.Password.RequireDigit = true;
                o.Password.RequireLowercase = false;
                o.Password.RequireUppercase = false;
                o.Password.RequireNonAlphanumeric = false;
                o.Password.RequiredLength = 10;
                o.User.RequireUniqueEmail = true;
            })
            .AddEntityFrameworkStores<DataContext>()
            .AddApiEndpoints()
            .AddDefaultTokenProviders();
        }

        public static void ConfigureServices(this IServiceCollection services, IConfiguration config)
        {
            services.AddControllers()
                .AddApplicationPart(typeof(Presentation.AssemblyReference).Assembly);

            services.AddSwaggerGen(c =>
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Web", Version = "v1" }));

            services.AddScoped<IServiceManager, ServiceManager>();

            services.AddScoped<IRepositoryManager, RepositoryManager>();

            services.AddTransient<ExceptionHandlingMiddleware>();
        }
    }
}
