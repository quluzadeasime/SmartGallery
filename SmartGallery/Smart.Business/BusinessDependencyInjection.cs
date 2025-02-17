using Microsoft.Extensions.DependencyInjection;
using Smart.Shared.Implementations;
using Smart.Shared.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smart.Business
{
    public static class BusinessDependencyInjection
    {
        public static IServiceCollection AddBusiness(this IServiceCollection services)
        {
            services.AddServices();
            services.RegisterAutoMapper();

            return services;
        }

        private static void AddServices(this IServiceCollection services)
        {
            services.AddScoped<IClaimService, ClaimService>();

            // Services adding here!!!
        }

        private static void RegisterAutoMapper(this IServiceCollection services)
        {
            //services.AddAutoMapper(typeof(BusinessDependencyInjection));
        }
    }
}
