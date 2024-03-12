using ESHRApp.Application.Interfaces;
using ESHRApp.Application.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ESHRApp.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IDepartmentService, DepartmentService>();
            services.AddScoped<IEducationService, EducationService>();
            services.AddScoped<IEmployeeService, EmployeeService>();
            services.AddScoped<IPromotionService, PromotionService>();
            return services;
        }
    }
}
