using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppServices.Interfaces;
using AppServices.Implementation;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;

namespace AppServices
{
    public static class SeviceInjection
    {
        public static IServiceCollection AddServiceInjection(this IServiceCollection services)
        {
            services.AddScoped<IUserService, UserService>();

            return services;
        }
    }
}
