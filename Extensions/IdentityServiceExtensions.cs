using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialFirstApi.Extensions
{
    public static class IdentityServiceExtensions
    {
        public static object Configuration { get; private set; }

        //IServiceCollection - Returning type
        //this - to extend IServiceCollection we're using
        public static IServiceCollection AddIdentityServices(this IServiceCollection services, IConfiguration config)
        {
            //For Authentication
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                //chain on configurations
                .AddJwtBearer(options =>
                {
                    //supply TokenValidationParameters
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        //Server is going to sign the token and we need to tell the server to validate the token 
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["TokenKey"])),
                        //API
                        ValidateIssuer = false,
                        //Angular app
                        ValidateAudience = false

                    };
                });

            return services;
        }
    }
}
