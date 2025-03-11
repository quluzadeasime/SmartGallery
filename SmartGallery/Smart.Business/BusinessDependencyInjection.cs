using CloudinaryDotNet;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Smart.Business.Helpers;
using Smart.Business.Services.ExternalServices.Abstractions;
using Smart.Business.Services.ExternalServices.Interfaces;
using Smart.Business.Services.InternalServices.Abstractions;
using Smart.Business.Services.InternalServices.Interfaces;
using Smart.Shared.Implementations;
using Smart.Shared.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Smart.Business
{
    public static class BusinessDependencyInjection
    {
        public static IServiceCollection AddBusiness(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddServices();
            services.AddControllers();
            services.RegisterAutoMapper();
            services.AddCloud(configuration);


            return services;
        }

        private static void AddServices(this IServiceCollection services)
        {

            // Internal Services

            services.AddScoped<IClaimService, ClaimService>();
            services.AddScoped<IBrandService, BrandService>();
            services.AddScoped<IColorService, ColorService>();
            services.AddScoped<IContactService, ContactService>();
            services.AddScoped<IServiceService, ServiceService>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<ISettingService, SettingService>();
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<ISubscriptionService, SubscriptionService>();
            services.AddScoped<ISpecificationService, SpecificationService>();

            // External Services
            services.AddScoped<IFileManagerService, FileManagerService>();
        }

        private static void RegisterAutoMapper(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(BusinessDependencyInjection));
        }

        private static void AddCloud(this IServiceCollection services, IConfiguration configuration)
        {
            var cloudName = configuration["Cloudinary:CloudName"];
            var cloudApiKey = configuration["Cloudinary:ApiKey"];
            var cloudApiSecret = configuration["Cloudinary:ApiSecret"];

            var account = new Account(cloudName, cloudApiKey, cloudApiSecret);
            var cloudinary = new Cloudinary(account);

            services.AddSingleton(cloudinary);
        }

        public class PluralizedRouteConvention : IControllerModelConvention
        {
            public void Apply(ControllerModel controller)
            {
                if(controller == null)
                    throw new ArgumentNullException(nameof(controller));

                var pluralizedName = NameHelper.PluralizeControllerName(controller.ControllerName);

                foreach(var selector in controller.Selectors)
                {
                    if(selector.AttributeRouteModel != null)
                    {
                        selector.AttributeRouteModel.Template = $"api/{pluralizedName}";
                    }
                }

            }
        }
    }
}
