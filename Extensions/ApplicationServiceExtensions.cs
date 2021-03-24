using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SocialFirstApi.Data;
using SocialFirstApi.Interfaces;
using SocialFirstApi.Services;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;

namespace SocialFirstApi.Extensions
{
    public static class ApplicationServiceExtensions
    {
        //IServiceCollection - Returning type
        //this - to extend IServiceCollection we're using
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration config)
        {
            //JWT
            /*AddScoped is scoped to the lifetime of the HTTP request*/
            services.AddScoped<ITokenService, TokenService>();
            //Adding the connection string for our database
            //AddDbContext<DataContext - Name of the class we created that's derived from DbContext
            /* => is a lamda function. Options is the parameter to which
             we pass the statements to inside te curly brackets*/
            //Inside UseMySql goes te "DefaultConnection" string
            services.AddDbContext<DataContext>(options
                   => options.UseMySql(config.GetConnectionString("DefaultConnection")));

            return services;
        }
    }
}
