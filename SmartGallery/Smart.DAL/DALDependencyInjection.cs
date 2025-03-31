using App.DAL.Presistence;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Smart.Core.Entities.Identity;
using Smart.DAL.Handlers.Implementations;
using Smart.DAL.Handlers.Interfaces;
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
            services.AddHandlers();
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
                .AddEntityFrameworkStores<AppDbContext>()
                .AddDefaultTokenProviders();  

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
                {typeof(IColorRepository),typeof(ColorRepository) },
                {typeof(IContactRepository),typeof(ContactRepository) },
                {typeof(ISettingRepository),typeof(SettingRepository) },
                {typeof(IServiceRepository),typeof(ServiceRepository) },
                {typeof(IProductRepository),typeof(ProductRepository) },
                {typeof(ICategoryRepository),typeof(CategoryRepository) },
                {typeof(ITransactionRepository),typeof(TransactionRepository) },
                {typeof(ISubscriptionRepository),typeof(SubscriptionRepository) },
                {typeof(IProductImageRepository),typeof(ProductImageRepository) },
                {typeof(IProductColorRepository),typeof(ProductColorRepository) },
                {typeof(ISpecificationRepository),typeof(SpecificationRepository) },
            };

            foreach(var (interfaceType, implementationType) in repositories)
            {
                services.AddScoped(interfaceType, implementationType);
            }
        }

        private static void AddHandlers(this IServiceCollection services)
        {
            var handlers = new Dictionary<Type, Type>
            {
                  {typeof(IColorHandler),typeof(ColorHandler) },
                  {typeof(IBrandHandler),typeof(BrandHandler) },
                  {typeof(IContactHandler),typeof(ContactHandler) },
                  {typeof(ISettingHandler),typeof(SettingHandler) },
                  {typeof(IServiceHandler),typeof(ServiceHandler) },
                  {typeof(IProductHandler),typeof(ProductHandler) },
                  {typeof(ICategoryHandler),typeof(CategoryHandler) },
                  {typeof(ITransactionHandler),typeof(TransactionHandler) },
                  {typeof(IProductImageHandler),typeof(ProductImageHandler) },
                  {typeof(IProductColorHandler),typeof(ProductColorHandler) },
                  {typeof(ISubscriptionHandler),typeof(SubscriptionHandler) },
                  {typeof(ISpecificationHandler),typeof(SpecificationHandler) } 

            };

            foreach (var handler in handlers)
            {
                services.AddScoped(handler.Key, handler.Value);
            }
        }

    }
}
