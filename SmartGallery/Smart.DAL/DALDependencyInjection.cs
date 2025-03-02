using App.DAL.Presistence;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Smart.Core.Entities.Identity;
using Smart.DAL.Repositories.Implementations;
using Smart.DAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smart.DAL
{
    public static class DALDependencyInjection
    {
        public static IServiceCollection AddDataAccess(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDatabase(configuration);
            services.AddIdentity();
            services.AddRepositories();
            services.AddHttpContextAccessor();

            return services;
        }

        private static void AddDatabase(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = Environment.GetEnvironmentVariable("CloudConnection")
                                     ?? configuration.GetConnectionString("DefaultConnection");

            services.AddDbContext<AppDbContext>(opt =>
            {
                opt.UseSqlServer(connectionString);
            });
        }

        private static void AddIdentity(this IServiceCollection services)
        {
            services.AddIdentity<User, IdentityRole>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddEntityFrameworkStores<AppDbContext>();

            services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequireUppercase = true;
                options.Password.RequiredLength = 8;
                options.Password.RequiredUniqueChars = 1;

                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.AllowedForNewUsers = true;

                options.User.AllowedUserNameCharacters =
                    "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
                options.User.RequireUniqueEmail = true;
            });
        }

        private static void AddRepositories(this IServiceCollection services)
        {
            //Repositories
            var repositories = new Dictionary<Type, Type> 
            {
                {typeof(IBrandRepository),typeof(BrandRepository) },
                {typeof(ICategoryRepository),typeof(CategoryRepository) },
                {typeof(IColorRepository),typeof(ColorRepository) },
                {typeof(IContactRepository),typeof(ContactRepository) },
                {typeof(ISettingRepository),typeof(SettingRepository) },
                {typeof(IServiceRepository),typeof(ServiceRepository) },
                {typeof(ISpecificationRepository),typeof(SpecificationRepository) },
                {typeof(IProductRepository),typeof(ProductRepository) },
                {typeof(IProductImageRepository),typeof(ProductImageRepository) },
                {typeof(IProductColorRepository),typeof(ProductColorRepository) }
            };

            foreach(var (interfaceType, implementationType) in repositories)
            {
                services.AddScoped(interfaceType, implementationType);
            }

            //services.AddScoped<IBrandRepository, BrandRepository>();

            //services.AddScoped<ICategoryRepository, CategoryRepository>();

            //services.AddScoped<IColorRepository, ColorRepository>();

            //services.AddScoped<IContactRepository, ContactRepository>();

            //services.AddScoped<ISettingRepository, SettingRepository>();

            //services.AddScoped<IServiceRepository, ServiceRepository>();

            //services.AddScoped<ISpecificationRepository, SpecificationRepository>();

            //services.AddScoped<IProductRepository, ProductRepository>();
            //services.AddScoped<IProductImageRepository, ProductImageRepository>();
            //services.AddScoped<IProductColorRepository, ProductColorRepository>();
        }
    }
}
