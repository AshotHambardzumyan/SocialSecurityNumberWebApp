using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using SocialSecurityNumberWebApp.Data.Data;
using Microsoft.Extensions.DependencyInjection;

namespace SocialSecurityNumberWebApp.Data.Injections
{
    public static class ContextInjection
    {
        public static IServiceCollection AddFakeDbContext(this IServiceCollection services)
        {
            services.AddSingleton<IUserDbContext, UserDbContext>();
            return services;
        }
    }
}