using DAL;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public static class ServiceRegistration
    {
        public static IServiceCollection AddBusinessServices(this IServiceCollection services)
        {
            services.AddScoped<IEmployeeService, EmployeeService>();
            services.AddScoped<IAddressService, AddressService>();
            services.AddScoped<IWarehouseService, WarehouseService>();

            services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            services.AddScoped<IAddressRepository, AddressRepository>();
            services.AddScoped<IWarehouseRepository, WarehouseRepository>();

            services.AddScoped<DbHelper>();

            return services;
        }
    }
}
