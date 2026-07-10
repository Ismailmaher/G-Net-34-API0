using E_Commerce.Application.Contracts;
using E_Commerce.Application.Service;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Application
{
    public static class ApplicationServicesRegesteration
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(ApplicationServicesRegesteration).Assembly);
            services.AddScoped<IProductService, ProductService>();
            return services;
        }
    }
}
