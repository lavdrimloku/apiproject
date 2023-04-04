using System.Text;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System;

namespace Web.Providers
{
    public static class AuthenticationExtension
    {
        public static IServiceCollection AddTokenAuthentication(this IServiceCollection services, IConfiguration config)
        {
            var secret = config.GetSection("JwtConfig").GetSection("secret").Value;
            var issuer = config.GetSection("JwtConfig").GetSection("issuer").Value;
 

            var key = Encoding.ASCII.GetBytes(secret);
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                
            })
            .AddJwtBearer(x =>
            {
                x.SaveToken = true;
               // x.Authority = issuer;
               // x.RequireHttpsMetadata = false;
              //  x.RequireHttpsMetadata = false;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false, 
                    //ValidIssuer = issuer,
                    //ValidAudience = issuer,
                    ValidateAudience = false, 
                    ClockSkew = TimeSpan.Zero,
                    ValidateIssuerSigningKey = true,
                   
                };
            });

            return services;
        }
    }
}
