using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.Extensions.DependencyInjection;
using Smart.Business.Helpers;
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
        public static IServiceCollection AddBusiness(this IServiceCollection services)
        {
            services.AddServices();
            services.RegisterAutoMapper();
            services.AddControllers();


            return services;
        }

        private static void AddServices(this IServiceCollection services)
        {
            services.AddScoped<IClaimService, ClaimService>();

            // Services adding here!!!
        }

        private static void RegisterAutoMapper(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(BusinessDependencyInjection));
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
